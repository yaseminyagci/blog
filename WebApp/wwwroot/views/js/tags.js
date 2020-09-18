

let dataTable;
const constants = {
    dataTableId: '#dataTable',

    addModalTitle: "Yeni Post Ekle",
    editModalTitle: "Post Düzenle",


    form: $("#form"),
    detailForm: $("#detailForm"),
    modal: $("#form").closest(".modal"),
    deleteModal: $("#deleteModal"),
    detailModal: $("#detailModal"),

    dataTableUrl: URLS.TAGS.GET_ALL,

    addUrl: URLS.TAGS.ADD,
    editUrl: URLS.TAGS.EDIT,
    getUrl: URLS.TAGS.GET,
    deleteUrl: URLS.TAGS.DELETE,


    //vehicleTypes: selector.getVehicleTypes(),

};


const functions = {
    reloadTable: () => {
        dataTable.ajax.reload();
    },


    setAllSelectData: () => {
        functions.setVehicleTypesSelectData();
        $('.bootstrap-select').selectpicker('refresh');
    },

    setVehicleTypesSelectData: () => {
        var arr = $.map(constants.vehicleTypes,
            function (val, index) {
                return { value: val.id, text: val.label };
            });
        var optionHtml = helper.buildOptionsHtmlString(arr);
        console.log(arr);
        $("#vehicleTypes").html(optionHtml);
    }
};
const main = {
    buildDataTable: () => {

        dataTable = new Plugins().DataTable(constants.dataTableId,
            {
                ajax: constants.dataTableUrl,

                columns: [
                    {
                        title: "Tag Name",
                        name: "tagName",
                        data: "tagName",
                        className: "showOnExport",
                        searchable: true,
                        sortable: true,
                    },
                 
                    {
                        title: "Aksiyon",
                        name: "options",
                        data: "id",
                        searchable: false,
                        sortable: false,
                        render: function (data, type, full, meta) {


                            let editHtml = ` <button type="button" class="dropdown-item editBtn" data-id="${data
                                }"><i class="la la-edit"></i> Düzenle </button>`
                            let deleteHtml = ` <button type="button" class="dropdown-item deleteBtn" data-id="${data
                                }"><i class="la la-trash"></i> Sil </button>`;


                            return ` <span class="dropdown">
                            <a href="#" class="btn btn-sm btn-clean btn-icon btn-icon-md" data-toggle="dropdown" aria-expanded="true">
                              <i class="la la-ellipsis-h"></i>
                            </a>
                            <div class="dropdown-menu dropdown-menu-right">
                                  <button type="button" class="dropdown-item detailBtn" data-id="${data
                                }">
                            <i class="la la-eye"></i> Görüntüle</button>
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
                helper.resetForm(constants.form);
                //$('.bootstrap-select').selectpicker('refresh');;
                $(constants.form).attr("action", constants.addUrl);
                $(constants.modal).modal("show");
                helper.setModalHeader(constants.modal, constants.addModalTitle);
            });



        $(document).on('click',
            '.detailBtn',
            function () {
                var id = $(this).attr("data-id");
                let url = constants.getUrl;
                request.Get(url + `/${id}`,
                    null,
                    function (response) {
                        helper.resetForm(constants.detailForm);         
                        $(constants.detailForm).deserialize(response.data);
                        $(constants.detailModal).modal("show");
                    });
               

            });

        $(document).on('click',
            '.editBtn',
            function () {
                console.log("vdfvfd");

                var id = $(this).attr("data-id");
                let url = constants.getUrl;
                request.Get(url + `/${id}`,
                    null,
                    function (response) {
                        helper.resetForm(constants.form);
                        $(constants.form).deserialize(response.data)
                        $('.bootstrap-select').selectpicker('refresh');

                        $(constants.form).attr("action", constants.editUrl);
                        $(constants.modal).modal("show");
                        helper.setModalHeader(constants.modal, constants.editModalTitle);
                    });

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
                    var url = $(this).attr('action');
                    KTApp.progress(btn);
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

                            $(constants.modal).modal("hide");
                            functions.reloadTable();
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
    //functions.setAllSelectData();
    // $('.bootstrap-select').selectpicker();
});
