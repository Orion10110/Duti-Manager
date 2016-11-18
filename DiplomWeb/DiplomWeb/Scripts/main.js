var events =[];

$(function () {
    GetEvents();
    var calendar = $('#calendar').fullCalendar({
        header: {
            left: 'prev,next today',
            center: 'title',
            right: 'month,basicWeek,basicDay'
        },
        events: events,
        color: 'yellow',   
        textColor: 'black',
        selectable: true,
        selectHelper: true,
        select: function (start, end, allDay) {
            var title = prompt('Event Title:');
            if (title) {
                start = start.format();
                end = end.format();
                $.ajax({
                    url: '/RecordVigils/SaveDate',

                    data: { startAt:start,endAt:end,title:title},
                    type: "POST",
                    success: function (json) {
                        alert('OK');
                    }
                });
                calendar.fullCalendar('renderEvent',
                {
                    title: title,
                    start: start,
                    end: end,
                    allDay:true,
                },
                true // make the event "stick"
                );
            }
            calendar.fullCalendar('unselect');
        },

        editable: true,
        eventDrop: function (event, delta) {
            start = event.start.format();
            end = event.end.format();
            $.ajax({
                url: '/RecordVigils/UpdateDate',
                data: {startAt:start,endAt:end,title:event.title},
                type: "POST",
                success: function (json) {
                    alert("OK");
                }
            });
        },
        eventResize: function (event) {
            start = event.start.format();
            end = event.end.format();
            $.ajax({
                url: '/RecordVigils/UpdateDate',
                data: { startAt: start, endAt: end, title: event.title},
                type: "POST",
                success: function (json) {
                    alert("OK");
                }
            });

        }
    });
});
      


function GetEvents() {
    $.ajax({
        async:false,
        url: '/RecordVigils/GetEvents',
        success: function (data) {
            $.each(data,function(index,value){
                events.push({
                    title: value.Title,
                    description:value.Description,
                    start: new Date(parseInt(value.StartAt.substr(6))),
                    end: new Date(parseInt(value.EndAt.substr(6))),
                    allDay: value.IsFullDay
                });    
            })
            }

        });
}

