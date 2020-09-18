
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

    addUrl: URLS.POST.ADD,
    editUrl: URLS.POST.EDIT,
    getUrl: URLS.POST.GET,
    deleteUrl: URLS.POST.DELETE,
    dataTableUrl: URLS.POST.GET_ALL,
    detailUrl: URLS.POST.DETAILPAGE,
    tagPosts: $("#tags"),
    selectedTags:[],
    index: URLS.POST.INDEX,
    tags:[],
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
                        title: "Title",
                        name: "title",
                        data: "title",
                        className: "showOnExport",
                        searchable: true,
                        sortable: true,
                    },
                    {
                        title: "Content",
                        data: "content",
                        defaultContent: "<button>Click!</button>",
                        type: "html",
                        
                    },       
                    //{
                    //    title: "Tags",
                    //    data: "tags",                         
                        
                    //},
                    {
                        title: "Aksiyon",
                        name: "options",
                        data: "id",
                        searchable: false,
                        sortable: false,
                        render: function (data, type, full, meta) {
                            

                            let editHtml = `  <a class="dropdown-item" href="${URLS.POST.EDITPAGE}${data}"><i class="la la - edit"></i> Düzenle</a>`
                            let deleteHtml = ` <button type="button" class="dropdown-item deleteBtn" data-id="${data
                                }"><i class="la la-trash"></i> Sil </button>`;


                            return ` <span class="dropdown">
                            <a href="#" class="btn btn-sm btn-clean btn-icon btn-icon-md" data-toggle="dropdown" aria-expanded="true">
                              <i class="la la-ellipsis-h"></i>
                            </a>
                            <div class="dropdown-menu dropdown-menu-right">
                                <a class="dropdown-item" href="${URLS.POST.DETAILPAGE}${data}"><i class="la la - edit"></i> Detay</a>
                         
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
 
        $('#dataTable tbody').on('click', 'button', function () {
            var data = dataTable.row($(this).parents('tr')).data();
            constants.detailForm.find('p[name="content"]').html(data.content);
            //constants.detailForm.find('p[name="content"]').prepend("2");
            constants.detailForm.deserialize(data);
            constants.detailModal.modal("show");
            console.log("data " + data.content);
    } );

        $(document).on("click",
                "#addBtn",
                function () {
                    window.location.href = URLS.POST.ADDPAGE;                
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
                    console.log("type",typeof (result.tags));
                    if (typeof (result.tags) == "string") {
                        constants.tags[0] = parseInt(result.tags);
                        result.tags = constants.tags;
                    }
                        console.log(result);
                    debugger;;
                   // KTApp.progress(btn);
                    request.Post(url,
                        result,
                        function (response) {
                            debugger;;
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
        main.buildDataTable();
        main.buildEvents();
    }
};
jQuery(document).ready(function () {
    main.init();
    functions.setTagsSelectData();
});
