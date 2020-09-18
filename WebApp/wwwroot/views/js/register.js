const constants = {
    form: $("#form"),
    login: URLS.HOME.LOGIN
};



const main = {
    buildEvents: () => {


        $(constants.form).on("submit",
            
            function (e) {

                e.preventDefault();
                var btn = $(this).find('button[type="submit"]');
                $(constants.form).serializeObject().then(result => {
                    var url = '/api/account/register';
                    request.Post(url,
                        result,
                        function (response) {

                            var message = response.message;
                            swal.fire({
                                "title": "",
                                "text": message,
                                "type": response.isError ? "warning" : "success",
                                "confirmButtonClass": "btn btn-secondary"
                            });
                            if(response.statusCode==200)
                            redirect(constants.login);
                        });

                });

            });
    },  

init: () => {
    main.buildEvents();
}
};
jQuery(document).ready(function () {
    main.init();
});
