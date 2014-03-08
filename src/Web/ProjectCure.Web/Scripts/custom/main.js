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

    $("#editUserModal").on("submit", "#editUserForm", function (e) {
        e.preventDefault();

        var $this = $(this);

        $.ajax({
            data: $this.serialize(),
            dataType: "text",
            type: "POST",
            url: $this.attr("action")
        }).then(function (response, textStatus, jqXHR) {
            //update modal contents with response, hide modal
            $("#editUserModal").empty().html(response).modal("hide");

        }, function (jqXHR, textStatus, errorThrown) {
            alert(errorThrown);
        });
    });

    $("#editUserModal").on("show.bs.modal", function (e) {
        var $modal = $(this);
        var $a = $(e.relatedTarget);

        //before showing, initiate a fetch of the user edit partial
        $.ajax({
            dataType: "text",
            type: "GET",
            url: $a.attr("href")
        }).then(function (response, textStatus, jqXHR) {
            $modal.empty().html(response);
        }, function (jqXHR, textStatus, errorThrown) {
            alert(errorThrown);
        });
    });

    $("#editUserModal").on("hidden.bs.modal", function (e) {
        //update the user list when the modal is hidden to 'refresh' the list with any changes made
        $.ajax({
            dataType: "text",
            type: "GET",
            url: "/User/ListPartial"
        }).then(function (response, textStatus, jqXHR) {
            $("#editUserModal").empty();
            $("#userList").find("tbody").empty().html(response);
            
        }, function (jqXHR, textStatus, errorThrown) {
            alert(errorThrown);
        });
    });

    $("#userList").on("click", "a.reset-password", function (e) {
        e.preventDefault();

        if (confirm("Reset user password\nAre you sure?")) {
            var userid = $(this).attr("data-user-id");

            $.ajax({
                data: { id: userid },
                dataType: "json",
                type: "POST",
                url: "/User/ResetPassword/"
            }).then(function (response, textStatus, jqXHR) {
                if (response.success) {
                    
                }
            }, function (jqXHR, textStatus, errorThrown) {
                alert(errorThrown);
            });
        }
    });
})