

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

    dataTableUrl: URLS.COMMENT.GET_ALL,

    getUrl: URLS.COMMENT.GET,
    deleteUrl: URLS.COMMENT.DELETE,


    //vehicleTypes: selector.getVehicleTypes(),

};

const functions = {
    reloadTable: () => {
        dataTable.ajax.reload();
    }
};

const main = {
    buildDataTable: () => {

        dataTable = new Plugins().DataTable(constants.dataTableId,
            {
                ajax: constants.dataTableUrl,

                columns: [
                    {
                        title: "Content",
                        name: "content",
                        data: "content",
                        className: "showOnExport",
                        searchable: true,
                        sortable: true,
                    },
                    {
                        title: "Full Name",
                        name: "fullName",
                        data: "userDetail.fullName",
                        className: "showOnExport",
                        searchable: true,
                        sortable: true,
                    },
                    {
                        title: "Title",
                        name: "title",
                        data: "post.title",
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
        $(document).on('click',
            '.deleteBtn',
            function () {
                debugger;
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

    },

    init: () => {
        main.buildDataTable();
        main.buildEvents();
    }
};
jQuery(document).ready(function () {
    main.init();
});
