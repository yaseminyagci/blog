
var Plugins = function () {

    this.DataTable = function ($dom, $options) {
        if (typeof $.fn.DataTable !== "undefined") {
            if ($dom === null || typeof $dom === "undefined") {
                $dom = '.dataTable';
            }
            if ($($dom).length === 0) {
                return false;
            }
            var defaultOptions = {
                scrollY: '50vh',
                scrollX: true,
                scrollCollapse: true,

                search: {
                    caseInsensitive:true,
                    smart: false
                },

                dom: 'lBfrtip',
                buttons: [
                    {
                        extend: 'print',
                        className: 'kt-hidden ',
                        attr: {
                            id: 'dtPrintButton'
                        },
                        exportOptions: {
                            columns: [".showOnExport"]
                        },
                    },
                    {
                        extend: 'csv',
                        className: 'kt-hidden ',
                        attr: {
                            id: 'dtCsvButton'
                        },
                        exportOptions: {
                            columns: [".showOnExport"]
                        },
                    },
                    {
                        extend: 'excel',
                        className: 'kt-hidden ',
                        attr: {
                            id: 'dtExcelButton'
                        },
                        exportOptions: {
                            columns: [".showOnExport"]
                        },

                    },
                    {
                        extend: 'pdf',
                        className: 'kt-hidden ',
                        attr: {
                            id: 'dtPdfButton'
                        },
                        exportOptions: {
                            columns: [".showOnExport"]
                        },
                        customize: function (doc) {
                            var colCount = new Array();
                            $($dom).find('tbody tr:first-child td').each(function () {
                                if ($(this).attr('colspan')) {
                                    for (var i = 1; i <= $(this).attr('colspan'); $i++) {
                                        colCount.push('*');
                                    }
                                } else { colCount.push('*'); }
                            });
                            doc.content[1].table.widths = colCount;
                        }
                    },
                    {
                        extend: 'copy',
                        className: 'kt-hidden ',
                        attr: {
                            id: 'dtCopyButton'
                        }
                    }
                ],

            }
            if ($options === null || typeof $options === "undefined") {
                $options = {
                }
            }
            $.extend($options, defaultOptions);
            return $($dom).DataTable($options);
        }
    }

    this.DateRangePicker = function($dom, $options) {
        if (typeof $.fn.daterangepicker !== "undefined") {
            if ($dom === null || typeof $dom === "undefined") {
                $dom = '.daterangepicker';
            }
            if ($($dom).length === 0) {
                return false;
            }
            var defaultOptions = {
                locale: {
                    format: 'DD/MM/YYYY',
                },
                buttonClasses: ' btn',
                applyClass: 'btn-primary',
                cancelClass: 'btn-secondary'
            }
            if ($options === null || typeof $options === "undefined") {
                $options = {
                }
            }
            $.extend($options, defaultOptions);
            return $($dom).daterangepicker($options);
        }

    }

}

