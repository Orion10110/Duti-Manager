 tinymce.init({ selector: 'textarea.tiny' });

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
        var html2Add = "<div ><span><input type='text' class = 'form-control col-md-10 elem' required name='groups' style=''/><a class='del_group'><i class='fa fa-times-circle' aria-hidden='true'></i></a></span></div>";

        $('#addGroup').append(html2Add);
    })
    $('#addGroup').on('click', '.del_group', function (e) {
        $(this).parent().remove();
    });

})
  
