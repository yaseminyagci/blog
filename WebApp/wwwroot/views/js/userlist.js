

let dataTable;
const constants = {
    dataTableId: '#dataTable',

    addModalTitle: "Yeni Post Ekle",
    form: $("#form"),
    deleteModal: $("#deleteModal"),


    addUrl: URLS.USERS.ADD,
    editUrl: URLS.USERS.EDIT,
    getUrl: URLS.USERS.GET,
    deleteUrl: URLS.USERS.DELETE,
    dataTableUrl: URLS.USERS.GET_ALL,
    detailUrl: URLS.USERS.DETAILPAGE,
    addPage: URLS.USERS.ADDPAGE,
    editPage: URLS.USERS.EDITPAGE,
    tagPosts: $("#tags"),
    selectedTags: [],

    tagsTypes: selector.getTags(),

};


const functions = {
    reloadTable: () => {
        dataTable.ajax.reload();
    },

    setAllSelectData: () => {
        functions.setVehicleTypesSelectData();
        $('.bootstrap-select').selectpicker('refresh');
    },

    setTagsSelectData: () => {
        var arr = $.map(constants.tagsTypes,
            function (val, index) {
                return { value: val.id, text: val.tagName };
            });
        $.each(arr, function (name, value) {
            console.log(name + '=' + value);
            $('#tags').append(`<option value="${value.value}"> 
                                       ${value.text} 
                                  </option>`);

        });

    }
};
const main = {
    buildDataTable: () => {
   
        dataTable = new Plugins().DataTable(constants.dataTableId,
            {
                ajax: constants.dataTableUrl,

                columns: [
                    {
                        title: "Email",
                        name: "email",
                        data: "email",
                        className: "showOnExport",
                        searchable: true,
                        sortable: true,
                    },
                    {
                        title: "UserName",
                        name: "userName",
                        data: "userName",                    
                        className: "showOnExport",
                        searchable: true,
                        sortable: true,
                    },
                    //{
                    //    title: "Password",
                    //    name: "password",
                    //    data: "password",
                    //    className: "showOnExport",
                    //    searchable: true,
                    //    sortable: true,                        

                    //},
                    {
                        title: "Aksiyon",
                        name: "options",
                        data: "id",
                        searchable: false,
                        sortable: false,
                        render: function (data, type, full, meta) {


                            let editHtml = `  <a class="dropdown-item" href="${constants.editPage}${data}"><i class="la la - edit"></i> Düzenle</a>`
                            let deleteHtml = ` <button type="button" class="dropdown-item deleteBtn" data-id="${data
                                }"><i class="la la-trash"></i> Sil </button>`;


                            return ` <span class="dropdown">
                            <a href="#" class="btn btn-sm btn-clean btn-icon btn-icon-md" data-toggle="dropdown" aria-expanded="true">
                              <i class="la la-ellipsis-h"></i>
                            </a>
                            <div class="dropdown-menu dropdown-menu-right">
                                <a class="dropdown-item" href="${constants.detailUrl}${data}"><i class="la la - edit"></i> Detay</a>
                         
                               ${1 ? editHtml : ""}
                                ${1 ? deleteHtml : ""}
                         
                            </div>
                        </span>  `;
                        }
                    }
                ]

            });

    },
    buildEvents: () => {

        $('#generalSearch').on('keyup click',
            function () {
                $(constants.dataTableId).DataTable().search($('#generalSearch').val()).draw();
            });
        $('#recordStatus').on('change',
            function () {
                $(constants.dataTableId).DataTable().columns(4).search($(this).val(), 'status').draw();
            });

        $(document).on("click",
            "#addBtn",
            function () {
                window.location.href = constants.addPage;
            });



        $(document).on('click',
            '.deleteBtn',
            function () {
                var id = $(this).data("id");
                $(constants.deleteModal).modal("show");
                $(constants.deleteModal).find('button[id="submit"]').data("id", id);
            });

        $(document).on('click',
            '#deleteModal button[id="submit"]',
            function () {
                debugger;;
                var id = $(this).data("id");
                let btn = $(this);
                var url = constants.deleteUrl;
                KTApp.progress(btn);
                request.Post(url + `/${id}`,
                    null,
                    function (response) {

                        var message = response.message;
                        swal.fire({
                            "title": "",
                            "text": message,
                            "type": response.isError ? "warning" : "success",
                            "confirmButtonClass": "btn btn-secondary"
                        });


                        $(constants.deleteModal).modal("hide");
                        functions.reloadTable();
                        KTApp.unprogress(btn);
                    });
            });


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
                            console.log(response);
                            var message = response.message;
                            swal.fire({
                                "title": "",
                                "text": message,
                                "type": response.isError ? "warning" : "success",
                                "confirmButtonClass": "btn btn-secondary"
                            });

                            //$(constants.modal).modal("hide");
                            //functions.reloadTable();
                            KTApp.unprogress(btn);
                        });

                });

            });

    },

    init: () => {
        main.buildDataTable();
        main.buildEvents();
    }
};
jQuery(document).ready(function () {
    main.init();
    functions.setTagsSelectData();
});
