const constants = {
    form: $("#form"),
    detailForm: $("#detailForm"),
    getUrl: '/api/post/getall',
    nextPost: $("#oldPost"),
};

function getDate(date) {
    date = date.substring(0, 10);
    return date;
}
var btn = $("#oldPost");
var value = 0;
main = {

    buildEvents: () => {
        constants.nextPost.on("click",
            function (e) {
                main.getDataToView(value);
                value += 1;

            });

        if (value == "0") {
            main.getDataToView(value);
            value += 1;
        }



    },
    getDataToView: (id) => {

        request.Get('/api/post/GetAllGuest/' + id,
            null,
            function (response) {
                console.log(response);
                for (var i = 0; i < response.data.length; i++) {
                    //var unEscapedResponse = unescape(response.data[i].content);
                    var unEscapedResponse = decodeURIComponent(response.data[i].content);

                    console.log("unEscaped", unEscapedResponse);
                    response.data[i].content = unEscapedResponse.substring(0, 25);
                    response.data[i].dateCreated = getDate(response.data[i].dateCreated);
                }

                for (var i = 0; i < response.data.length; i++) {

                    var hmtlString = '<div class="post-preview"><h2 class="post-title" id="title"><a href="/detail/' + response.data[i].id + '">' + response.data[i].title + '</a></h2><h3 class="post-subtitle" id="content">' + response.data[i].content + '</h3><p> <a href="/detail/' + response.data[i].id + '"><em style="color:tomato;"> to be countinued</em></a></p><p class="post-meta">Posted <a href="#">Start Bootstrap</a>' + response.data[i].dateCreated + '</p></div >';
                    $(form).find('div[id="posts"]').append(hmtlString);
                }

            });
    },
}

jQuery(document).ready(function () {
    main.buildEvents();
});