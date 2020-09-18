let dataTable;
const constants = {
    comment: {
        user: "",
        date: "",
        comment: ""
    },

    form: $("#form"),
    likeForm: $("#likeForm"),
    addUrl: URLS.COMMENT.ADD,
 
    likesAddUrl: URLS.LIKES.ADD,
    likesGetUrl: URLS.LIKES.GETBYPOST,
    likesEditUrl: URLS.LIKES.EDIT,
    likeCount: $("#likeCount"),
    likedId: $("#likedId"),
    likedButton: $("#liked"),
    tagPosts: $("#tags"),
    inputDescription: $("#inputDescription"),
    selectedTags: [],
    postId: $("#PostId"),
    comments: {},

};

const functions = {

    getLikes: () => {
        
        debugger;;

        var btn = constants.likedButton;
            request.Get(constants.likesGetUrl + constants.postId.val(),
            null,
            function (response) {
                console.log(response);
                likeCount.innerText="";
                likeCount.append(`Liked ${response.data.likeCount}`);
                $(constants.likedId).val(response.data.id);
                if (response.data.liked == true) {
                    btn[0].classList.remove("btn-default");
                    btn[0].classList.add("btn-clicked");

                    btn.val("false");
                }
                else if (response.data.liked == false) {
                    btn[0].classList.add("btn-default");
                    btn[0].classList.remove("btn-clicked");
                 
                    btn.val("true");               
               
                }
             
            });
    },

    getDate: (date) => {
        date = date.substring(0, 10);
        return date;
    },

    addComment: (comment) => {
        $('#comments').append(` <div class="card-comment">
                <!-- User image -->
                <div class="comment-text">
                    <span class="username">
                        <b>${comment.user}</b>
                        <span class="text-muted float-right">${comment.date}</span>
                    </span><!-- /.username -->
                   </br>${comment.comment}
                </div>
                <!-- /.comment-text -->
            </div>`)
    },

    setTagsSelectData: () => {
        constants.comments = selector.getComments(constants.postId[0].value);
        console.log("tagTypes", constants.comments);

        var arr = $.map(constants.comments,
            function (val, index) {
                return { value: val.id, comment: val.content, createdDate: functions.getDate(val.dateCreated), user: val.userDetail.fullName };
            });
        $.each(arr, function (name, value) {
            console.log(name + '=' + value);

            functions.addComment({ user: value.user, date: value.createdDate, comment: value.comment });

        });
    }

};

const main = {

    buildEvents: () => {

        $(constants.form).on("submit",
            function (e) {

                e.preventDefault();
                var btn = $(this).find('button[type="submit"]');
                
                    $(constants.form).serializeObject().then(result => {
                        //var url = $(this).attr('action');
                        var url = constants.addUrl;
                        console.log(result);
                        // KTApp.progress(btn);
                        request.Post(url,
                            result,
                            function (response) {
                                debugger;;
                                console.log(response);
                                debugger;
                                functions.addComment({ user: response.data.userDetail.fullName, date: functions.getDate(response.data.dateCreated), comment: response.data.content });

                                constants.inputDescription.val("");
                                functions.getLikes();
                            });

                    });
                

                
            });
        $(constants.likeForm).on("submit",
            function (e) {
                debugger;;
                e.preventDefault();
                var btn = constants.likedButton;
                if (constants.likedId.val()=="0") {
                    btn[0].classList.remove("btn-default");
                    btn[0].classList.add("btn-clicked");

                    btn.val("true");
                    
                    $(constants.likeForm).serializeObject().then(result => {
                        var url = constants.likesAddUrl;
                        result.liked = constants.likedButton.val();
                        console.log("result" + result);
                        // KTApp.progress(btn);
                        request.Post(url,
                            result,
                            function (response) {
                                console.log(response);
                                debugger;
                                functions.getLikes();
                            });

                    });
                }
                else {
                    $(constants.likeForm).serializeObject().then(result => {
                        var url = constants.likesEditUrl;
                        console.log("result" + result);
                        result.liked = constants.likedButton.val();
                        debugger;;
                        // KTApp.progress(btn);
                        console.log("likedId " + likedId.value);
                        console.log("liked " + result.liked);
                        request.Post(url,
                            result,
                            function (response) {
                                console.log(response);
                                debugger;
                                functions.getLikes();
                            });

                    });
                }
            });
    },

    init: () => {
        functions.getLikes();
        main.buildEvents();
    }
};

jQuery(document).ready(function () {
    main.init();
    functions.setTagsSelectData();
   
});
