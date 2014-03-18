﻿$(document).ready(function () {
    $('#calendar').fullCalendar({
        height: $(window).height() - 120,
        events: '/events/',
        eventRender: function (event, element) {
            element.find('.fc-event-title').html(moment(event.start).format("h A") + ' ' + event.title);
        },
        eventClick: function (event) {
            if (event.url) {
                $("#evntModal").modal({
                    remote: event.url
                });
            }
            return false;
        },
        dayClick: function (date) {
            if (!isAdmin) return;
            $("#evntModal").modal({
                remote: "/events/create?date=" + moment(date).format("YYYY-MM-DD")
            });
        }
    });

    $('body').on('hidden.bs.modal', '.modal', function () {
        $(this).removeData('bs.modal');
    });

    $(document).on("click", "#eventForm .claim", function () {
        var choice = confirm(this.getAttribute('data-confirm'), 'yes', 'no');
        if (choice) {
            var url = $(this).parents("form").attr("action"); // the script where you handle the form input.

            $.ajax({
                type: "PUT",
                url: url,
                data: $(this).parents("form").serialize(), // serializes the form's elements.
                success: function (data) {
                    $("#evntModal").modal("hide");
                    window.location.reload();
                },
                error: function (data) {
                    alert("Failed to save event!");
                }
            });
        }
        event.preventDefault(); // avoid to execute the actual submit of the form.
    });

    $(document).on("click", "#eventForm .cancel", function () {
        var choice = confirm(this.getAttribute('data-confirm'), 'yes', 'no');
        if (choice) {
            var url = $(this).parents("form").attr("action"); // the script where you handle the form input.

            $.ajax({
                type: "PUT",
                url: url,
                data: { Action: "Delete" }, // serializes the form's elements.
                success: function (data) {
                    $("#evntModal").modal("hide");
                    window.location.reload();
                },
                error: function (data) {
                    alert("Failed to cancel event!");
                }
            });
        }
        event.preventDefault();
    });

    $(document).on("click", "#createEventForm .createBtn", function () {
        var url = '/events/';

        $.ajax({
            type: "POST",
            url: url,
            data: $(this).parents("form").serialize(), // serializes the form's elements.
            success: function (data) {
                $("#evntModal").modal("hide");
                window.location.reload();
            },
            error: function (data) {
                alert("Failed to create event!");
            }
        });
        event.preventDefault();
    });
});