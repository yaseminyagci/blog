

let dataTable;
const constants = {
    dataTableId: '#dataTable',

    addModalTitle: "Yeni Araç Ekle",
    editModalTitle: "Araç Düzenle",


    form: $("#form"),
    detailForm: $("#detailForm"),
    modal: $("#form").closest(".modal"),
    deleteModal: $("#deleteModal"),
    detailModal: $("#detailModal"),
    maintenanceModal: $("#maintenanceModal"),
    unMaintenanceModal: $("#unMaintenanceModal"),

    dataTableUrl: URLS.VEHICLE.GET_ALL,

    addUrl: URLS.VEHICLE.ADD,
    editUrl: URLS.VEHICLE.EDIT,
    getUrl: URLS.VEHICLE.GET,
    deleteUrl: URLS.VEHICLE.DELETE,
    maintenanceUrl: URLS.VEHICLE.MAINTENANCE,
    unMaintenanceUrl: URLS.VEHICLE.UNMAINTENANCE,

    vehicleTypes: selector.getVehicleTypes(),

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
            function(val, index) {
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
                        title: "Araç",
                        name: "label",
                        data: "label",
                        className: "showOnExport",
                        searchable: true,
                        sortable: true,
                    },
                    {
                        title: "Araç Tipi",
                        name: "vehicleType",
                        data: "vehicleType",
                        className: "showOnExport",
                        searchable: true,
                        sortable: true,
                        render: function(data, type, full, meta) {

                            return data.label;
                        }
                    },
                    {
                        title: "Terminal Id",
                        name: "terminalId",
                        data: "terminalId",
                        className: "showOnExport",
                        searchable: true,
                        sortable: true,
                       },
                    {
                        title: "Bağlı Cihaz",
                        name: "device",
                        data: "device",
                        className: "showOnExport",
                        searchable: true,
                        sortable: true,
                        render: function(data, type, full, meta) {
                            if (data)
                                return data.label;
                            else
                                return "";
                        }
                    },
                    {
                        title: "Durum",
                        name: "status",
                        data: "status",
                        className: "showOnExport",
                        searchable: true,
                        sortable: true,
                        render: function(data, type, full, meta) {
                            return helper.renderTableStatusContent(data);
                        }
                    },
                    {
                        title: "Aksiyon",
                        name: "options",
                        data: "id",
                        searchable: false,
                        sortable: false,
                        render: function(data, type, full, meta) {


                            let editHtml = ` <button type="button" class="dropdown-item editBtn" data-id="${data
                                }"><i class="la la-edit"></i> Düzenle </button>`
                            let deleteHtml = ` <button type="button" class="dropdown-item deleteBtn" data-id="${data
                                }"><i class="la la-trash"></i> Sil </button>`;


                            var isInMaintenance = full.status === 4;
                            var maintananceHtml = isInMaintenance
                                ? `  <button type="button"  class="dropdown-item setUnMaintenance" data-id="${data
                                }"><i class="la la-check-circle"></i> Arızayı Bitir</button>`
                                : `  <button type="button"  class="dropdown-item setMaintenance" data-id="${data
                                }"><i class="la la-ban"></i> Arıza/Bakım Modu</button>`;


                            return ` <span class="dropdown">
                            <a href="#" class="btn btn-sm btn-clean btn-icon btn-icon-md" data-toggle="dropdown" aria-expanded="true">
                              <i class="la la-ellipsis-h"></i>
                            </a>
                            <div class="dropdown-menu dropdown-menu-right">
                                  <button type="button" class="dropdown-item detailBtn" data-id="${data
                                }">
                            <i class="la la-eye"></i> Görüntüle</button>
                               ${canEdit ? editHtml : ""}
                                ${canDelete ? deleteHtml : ""}
                                ${canEdit ? maintananceHtml : ""}
                            </div>
                        </span>  `;
                        }
                    }
                ]

            });

    },
    buildEvents: () => {

        $('#generalSearch').on('keyup click',
            function() {
                $(constants.dataTableId).DataTable().search($('#generalSearch').val()).draw();
            });
        $('#recordStatus').on('change',
            function() {
                $(constants.dataTableId).DataTable().columns(4).search($(this).val(), 'status').draw();
            });
        if (canAdd) {
            $(document).on("click",
                "#addBtn",
                function() {

                    helper.resetForm(constants.form);
                    $('.bootstrap-select').selectpicker('refresh');;
                    $(constants.form).attr("action", constants.addUrl);
                    $(constants.modal).modal("show");
                    helper.setModalHeader(constants.modal, constants.addModalTitle);
                });
        }


        $(document).on('click',
            '.detailBtn',
            function() {
                var id = $(this).attr("data-id");
                let url = constants.getUrl;
                request.Get(url + `/${id}`,
                    null,
                    function(response) {
                        helper.resetForm(constants.detailForm);
                        let data = response.data;
                        if (data.status === 1)
                            data.status = "Aktif"
                        else if (data.status === 4)
                            data.status = "Arızalı"
                        data.vehicleType = data.vehicleType.label;
                        data.device = data.device.label;
                        $(constants.detailForm).deserialize(response.data);
                        let statusInput = $(constants.detailForm).find('input[name="status"]');
                        let badge = $(statusInput).closest(".kt-badge--success ");
                        $(badge).removeClass("kt-badge--success");
                        $(badge).addClass("kt-badge--danger");

                    });
                $(constants.detailModal).modal("show");

            });


        if (canEdit) {
            $(document).on('click',
                '.editBtn',
                function() {


                    var id = $(this).attr("data-id");
                    let url = constants.getUrl;
                    request.Get(url + `/${id}`,
                        null,
                        function(response) {
                            helper.resetForm(constants.form);
                            $(constants.form).deserialize(response.data)
                            $('.bootstrap-select').selectpicker('refresh');

                            $(constants.form).attr("action", constants.editUrl);
                            $(constants.modal).modal("show");
                            helper.setModalHeader(constants.modal, constants.editModalTitle);
                        });

                });
            $(document).on('click',
                '.setMaintenance',
                function() {
                    var id = $(this).data("id");
                    $(constants.maintenanceModal).modal("show");
                    $(constants.maintenanceModal).find('button[id="submit"]').data("id", id);
                });

            $(document).on('click',
                '.setUnMaintenance',
                function() {
                    var id = $(this).data("id");
                    $(constants.unMaintenanceModal).modal("show");
                    $(constants.unMaintenanceModal).find('button[id="submit"]').data("id", id);
                });

            $(document).on('click',
                '#maintenanceModal button[id="submit"]',
                function () {
                    var id = $(this).data("id");
                    let btn = $(this);
                    var url = constants.maintenanceUrl;
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


                            $(constants.maintenanceModal).modal("hide");
                            functions.reloadTable();
                            KTApp.unprogress(btn);
                        });
                });
            $(document).on('click',
                '#unMaintenanceModal button[id="submit"]',
                function () {
                    var id = $(this).data("id");
                    let btn = $(this);
                    var url = constants.unMaintenanceUrl;
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


                            $(constants.unMaintenanceModal).modal("hide");
                            functions.reloadTable();
                            KTApp.unprogress(btn);
                        });
                });
        }

        if (canDelete) {
            $(document).on('click',
                '.deleteBtn',
                function() {
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
            function(e) {
                e.preventDefault();
                var btn = $(this).find('button[type="submit"]');
                $(constants.form).serializeObject().then(result => {
                    var url = $(this).attr('action');
                    KTApp.progress(btn);
                    request.Post(url,
                        result,
                        function(response) {

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
    functions.setAllSelectData();
    $('.bootstrap-select').selectpicker();
});
