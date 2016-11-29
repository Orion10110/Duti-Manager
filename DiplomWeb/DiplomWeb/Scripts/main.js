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
        editable: true,
        select: function (start, end) {

            $('#ModalAdd #id').val(event._id);
            $('#ModalAdd #start').val(moment(start).format('YYYY-MM-DD HH:mm:ss'));
            $('#ModalAdd #end').val(moment(end).format('YYYY-MM-DD HH:mm:ss'));
            $('#ModalAdd').modal('show');
        },

        eventRender: function (event, element) {
            element.bind('dblclick', function () {
                if (event.editable!==false) {
                    $('#ModalEdit #id').val(event._id);
                    $('#DeleteLink').attr('data-delet-id', event._id);
                    $('#ModalEdit #title').val(event.title);
                    $('#ModalEdit #color').val(event.color);
                    $('#ModalEdit').modal('show');
                }
            });
        },
        eventDrop: function (event, delta, revertFunc) { // si changement de position

            edit(event);

        },
        eventResize: function (event, dayDelta, minuteDelta, revertFunc) { // si changement de longueur

            edit(event);

        }

        //eventDrop: function (event, delta) {
        //    start = event.start.format();
        //    end = event.end.format();
        //    $.ajax({
        //        url: '/RecordVigils/UpdateDate',
        //        data: {startAt:start,endAt:end,title:event.title},
        //        type: "POST",
        //        success: function (json) {
        //            alert("OK");
        //        }
        //    });
        //},
        //eventResize: function (event) {
        //    start = event.start.format();
        //    end = event.end.format();
        //    $.ajax({
        //        url: '/RecordVigils/UpdateDate',
        //        data: { startAt: start, endAt: end, title: event.title},
        //        type: "POST",
        //        success: function (json) {
        //            alert("OK");
        //        }
        //    });

        //}
    });

    $('#addEvent').submit(function (e) {
        var $form = $(this);
        $.ajax({
            type: $form.attr('method'),
            url: $form.attr('action'),
            data: $form.serialize()
        }).done(function (data) {
            console.log('success');
            calendar.fullCalendar('renderEvent',
                 {
                     id:data.Id,
                     title: data.title,
                     start: new Date(parseInt(data.start.substr(6))),
                     end: new Date(parseInt(data.end.substr(6))),
                     color:data.color,
                     allDay:true
                 },
                 true // make the event "stick"
                 );
            $("#ModalAdd").find(".closing").trigger("click");
        }).fail(function () {
            console.log('fail');

        });
        //отмена действия по умолчанию для кнопки submit
        e.preventDefault();
    });

    $('#changeEvent').submit(function (e) {
        var $form = $(this);
        $.ajax({
            type: $form.attr('method'),
            url: $form.attr('action'),
            data: $form.serialize()
        }).done(function (data) {
            console.log('success');
            //if (data.delete = true) {
            //    calendar.fullCalendar( 'removeEvents', data.id)
            //} else {
            var event =  calendar.fullCalendar('clientEvents', data.id);
                event[0].title = data.title;
                event[0].color = data.color;
                calendar.fullCalendar('updateEvent', event[0]);
            //}

            $("#ModalEdit").find(".closing").trigger("click");
        }).fail(function () {
            console.log('fail');

        });
        //отмена действия по умолчанию для кнопки submit
        e.preventDefault();
    });

    $('#DeleteLink').click(function (e) {
       
        var url = $(this).attr('href');
        var id = $(this).attr('data-delet-id');
        $.ajax({
            type: "POST",
            url: url,
            data:{id:id},
        }).done(function (data) {
            console.log('success');
         calendar.fullCalendar( 'removeEvents', data.id)

           

            $("#ModalEdit").find(".closing").trigger("click");
        }).fail(function () {
            console.log('fail');

        });
        //отмена действия по умолчанию для кнопки submit
        e.preventDefault();
    });
});
      
function edit(event) {
    start = event.start.format('YYYY-MM-DD HH:mm:ss');
    if (event.end) {
        end = event.end.format('YYYY-MM-DD HH:mm:ss');
    } else {
        end = start;
    }

    id = event.id;

    $.ajax({
        url: '/UserVigils/UpdateDate',
        type: "POST",
        data: {id:id, startAt: start, endAt: end },
        success: function (rep) {
           
                alert('Saved');
           
        }
    });
}

function GetEvents() {
    $.ajax({
        async:false,
        url: '/UserVigils/GetEvents',
        success: function (data) {
            $.each(data,function(index,value){
                events.push({
                    id:value.Id,
                    title: value.Title,
                    description:value.Description,
                    start: new Date(parseInt(value.StartAt.substr(6))),
                    end: new Date(parseInt(value.EndAt.substr(6))),
                    allDay: value.IsFullDay,
                    color: value.Color,
                    editable: value.Editable
                });    
            })
            }

        });
}

