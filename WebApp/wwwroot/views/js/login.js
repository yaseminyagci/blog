const constants = {
    homeUrl: URLS.HOME.INDEX,
    guestUrl: URLS.HOME.GUEST,
    form: $("#form")
};
const main = {
    buildEvents: () => {
        $(constants.form).on("submit",
            function (e) {
                e.preventDefault();
                var btn = $(this).find('button[type="submit"]');
                $(constants.form).serializeObject().then(result => {
                    var url = '/api/account/login';
                    request.Post(url,
                        result,
                        function (response) {
                            if (response.data.detail.userTypeId == 1 || response.data.detail.userTypeId== 3) {
                                    redirect(constants.homeUrl);
                                    //window.location(constans.homeUrl);
                                                                  
                                }
                                else
                                    redirect(constants.guestUrl);
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
