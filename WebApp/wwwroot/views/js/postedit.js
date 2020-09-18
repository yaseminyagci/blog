const constants = {
    form: $("#form"),
    detailForm: $("#detailForm"),
    modal: $("#form").closest(".modal"),
    deleteModal: $("#deleteModal"),
    detailModal: $("#detailModal"),

    addUrl: URLS.POST.ADD,
    editUrl: URLS.POST.EDIT,
    getUrl: URLS.POST.GET,
    index: URLS.POST.INDEX,

    tagPosts: $("#tags"),
    id: $("#id"),
    selectedTags: selector.getSelectedTags(id.value),
};


const functions = {

    initData: () => {

        let url = constants.getUrl;
        request.Get(url + `/${id}`,
            null, function (response) {
                helper.resetForm(constants.form);
                $(constants.form).deserialize(response.data);
                $(constants.form).attr("action", constants.editUrl);
                $(constants.modal).modal("show");
                helper.setModalHeader(constants.modal, constants.editModalTitle);
            });

    },
 
    setTagsSelectData: () => {
        var arr = $.map(constants.selectedTags,
            function (val, index) {
                return { value: val.tag.id, text: val.tag.tagName,selected:val.selected };
            });
        $.each(arr, function (name, value) {
            console.log(name + '=' + value);
            if (value.selected==true)
            $('#tags').append(`<option selected value="${value.value}"> 
                                       ${value.text} 
                                  </option>`);
            else
                $('#tags').append(`<option value="${value.value}"> 
                                       ${value.text} 
                                  </option>`);
        });
   
    }
};
let formEl;

const main = {
    buildEvents: () => {

        



           formEl.on("submit",
            function (e) {
                e.preventDefault();
                var btn = $("#editBtn");
                formEl.serializeObject().then(result => {
                    //var url = $(this).attr('action');
                    var url = constants.editUrl;
                    console.log(result);
                       
                    // KTApp.progress(btn);
                    request.Post(url,
                        result,
                        function (response) {
                            console.log(response);
                            var message = response.message;
                            swal.fire({
                                "title": "",
                                "text": message,
                                "type": response.isError ? "warning" : "success",
                                "confirmButtonClass": "btn btn-secondary"
                            });
                            window.location.url(constants.index);
                            //$(constants.modal).modal("hide");
                            //functions.reloadTable();
                            KTApp.unprogress(btn);
                        });

                });

            });
    },


    init: () => {
        formEl = $('#editform');
        main.buildEvents();
        functions.setTagsSelectData();
    }
}

jQuery(document).ready(function () {
    main.init();
});














































































           
