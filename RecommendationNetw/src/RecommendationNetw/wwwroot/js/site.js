$(document).ready(function(){
    $('[data-toggle="tooltip"]').tooltip({
        placement: 'top'
    });

    $(function () {
        $.ajaxSetup({ cache: false });
        $(".deleteButton").click(function (e) {

            e.preventDefault();
            $.get(this.href, function (data) {
                $('#dialogContent').html(data);
                $('#modDialog').modal('show');
            });
        });
    })
});