 tinymce.init({ selector: 'textarea' });

$(document).ready(function () {
   

    $("#fileUploader").change(function () {
        var files = $(this)[0].files;
        for (var i = 0; i < files.length; i++) {
            $("#upload-file-info").append("<span class='label label-info' style='margin-left:10px' >" + files[i].name + "</span>");
        }
    });

    var i = 0;
    $('.addLink').click(function () {
        i++;
        var html2Add = "<div><input type='text' class = 'form-control' required name='groups' style='display:inline'/><a class='del_group'>x</a></div>";

        $('#addGroup').append(html2Add);
    })
    $('#addGroup').on('click', '.del_group', function (e) {
        $(this).parent().remove();
    });

})
  
