

jQuery(document).ready(function () {
    $($("#logoutForm")).on("submit",
        function (e) {
            e.preventDefault();
            var btn = $(this).find('button[type="submit"]');
            var url = URLS.ACCOUNT.LOGOUT;
            request.Post(url,
                null,
                function (response) {

                    var message = response.message;
                    swal.fire({
                        "title": "",
                        "text": message,
                        "type": response.isError ? "warning" : "success",
                        "confirmButtonClass": "btn btn-secondary"
                    });
                    if (!response.isError)
                        redirect(URLS.ACCOUNT.LOGIN);
                   
                });


        });
});
