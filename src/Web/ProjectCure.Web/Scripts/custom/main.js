$(function () {
    $("#log-off").click(function () {
        $("#logoutForm").submit();
    });

    $("form").on("submit", "#editUserForm", function (e) {
        e.preventDefault();

        var $this = $(this);

        $.ajax({
            data: $this.serialize(),
            dataType: "text",
            type: "POST",
            url: $this.attr("action")
        }).then(function (response, textStatus, jqXHR) {
            $("#myModal").empty().html(response);

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