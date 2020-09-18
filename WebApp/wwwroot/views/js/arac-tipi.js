
let dataTable;
const constants = {
    dataTableId: '#dataTable',

    addModalTitle: "Yeni Araç Tipi Ekle",
    editModalTitle: "Araç Tipi Düzenle",

    form: $("#form"),
    modal: $("#form").closest(".modal"),
    deleteModal: $("#deleteModal"),
    dataTableUrl: URLS.VEHICLE_TYPE.GET_ALL,

    addUrl: URLS.VEHICLE_TYPE.ADD,
    editUrl: URLS.VEHICLE_TYPE.EDIT,
    getUrl: URLS.VEHICLE_TYPE.GET,
    deleteUrl: URLS.VEHICLE_TYPE.DELETE

}


const functions = {
    reloadTable: () => {
        dataTable.ajax.reload();
    }
}
const main = {
    buildDataTable: () => {

        dataTable = new Plugins().DataTable(constants.dataTableId, {
            ajax: constants.dataTableUrl,


            columns: [
                {
                    title: "Araç Tipi",
                    name: "label",
                    data: "label",
                    className: "showOnExport",
                    searchable: true,
                    sortable: true,
                },
                {
                    title: "Açıklama",
                    name: "description",
                    data: "description",
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
                        if (canEdit || canDelete) {
                            return ` <span class="dropdown">
                            <a href="#" class="btn btn-sm btn-clean btn-icon btn-icon-md" data-toggle="dropdown" aria-expanded="true">
                              <i class="la la-ellipsis-h"></i>
                            </a>
                            <div class="dropdown-menu dropdown-menu-right">
                                ${canEdit ? editHtml : ""}
                                ${canDelete ? deleteHtml : ""}
                            </div>
                        </span>  `;
                        }
                        else {
                            return `\
                        <div class="dropdown">\
                            <a href="javascript:;" class="btn btn-sm btn-clean btn-icon btn-icon-md" disabled>\
                                <i style="color:red;" class="la la-cog"></i>\
                            </a>\                         
                        </div>\
                        `;
                        }
                     

                    }
                }
            ]

        });

    },
    buildEvents: () => {


        $('#generalSearch').on('keyup click', function () {
            $(constants.dataTableId).DataTable().search($('#generalSearch').val()).draw();
        });
        if (canAdd) {
            $(document).on("click",
                "#addBtn",
                function () {

                    helper.resetForm(constants.form);
                    $(constants.form).attr("action", constants.addUrl);
                    $(constants.modal).modal("show");
                    helper.setModalHeader(constants.modal, constants.addModalTitle);
                });
        }
        if (canEdit) {
            $(document).on('click',
                '.editBtn',
                function () {


                    var id = $(this).attr("data-id");
                    let url = constants.getUrl;
                    request.Get(url + `/${id}`,
                        null, function (response) {
                            helper.resetForm(constants.form);
                            $(constants.form).deserialize(response.data);
                            $(constants.form).attr("action", constants.editUrl);
                            $(constants.modal).modal("show");
                            helper.setModalHeader(constants.modal, constants.editModalTitle);
                        });

                });
        }
        if (canDelete) {
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
        }
  

      

       

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
}

jQuery(document).ready(function () {
    main.init();
});
