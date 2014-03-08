$(function () {
    $("#log-off").click(function () {
        $("#logoutForm").submit();
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
            var $modal = $("#editUserModal");
            $modal.empty().html(response);
            
            if ($modal.find("div.validation-summary-errors ul li").length === 0) {
                $modal.modal("hide");
            }
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

    $("#editUserModal").on("click", "#IsActive", function () {
        var checked = $(this).prop("checked");
        var originalChecked = $(this).attr("data-orig") === "true";

        var $events = $("#futureEvents");
        if ($events.length > 0) {
            $events.toggle(originalChecked && !checked);
        }
    });

    //Unfilled Event Notifications
    $("#unfilledEventsNotification").click(function (e) {

        var btn = $(this);
        btn.button('loading');
        $.ajax({
            dataType: "json",
            type: "POST",
            url: "/User/UnfilledEventsNotification/"
        }).then(function (response, textStatus, jqXHR) {
            if (response.success) {
                alert(response.message);
            }
        }, function (jqXHR, textStatus, errorThrown) {
            alert(errorThrown);
        }).always(function () {
            btn.button('reset');
        });

        return false;
    });
})