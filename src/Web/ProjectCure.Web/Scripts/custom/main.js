$(function () {
    $("#log-off").click(function () {
        $("#logoutForm").submit();
    });
    
    //full calendar
    $('#calendar').fullCalendar({
        height: $(window).height() - 120,
        events: '/data.json',
        eventRender: function (event, element) {
            var time = "3 PM ";
            element.find('.fc-event-title').html(time + event.title);
        },
        eventClick: function (event) {
            if (event.url) {
                $("#calendar-modal").modal({
                    remote: event.url
                });
                return false;
            }
        }
    })

    //calendar event clicks
    $(document).on('click', '.claim', function(event) {
        event.preventDefault();

        var choice = confirm(this.getAttribute('data-confirm'), 'yes', 'no');

        if (choice) {
            $(this).submit(function() {

                var url = "path/to/your/script.php"; // the script where you handle the form input.

                $.ajax({
                    type: "POST",
                    url: url,
                    data: $("#idForm").serialize(), 
                    success: function(data){
                        alert(data);
                    }
                });

                return false; 
            });
        }
    });
})