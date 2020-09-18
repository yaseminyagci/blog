
$(function () {
    //$("form").on("keyup keypress",
    //    function (e) {
    //        var keyCode = e.keyCode || e.which;
    //        if (keyCode === 13) {
    //            e.preventDefault();
    //            return false;
    //        }
    //    });



    $(document).on('click',
        "#exportPdf",
        function () {
            $("#dtPdfButton").trigger("click");
        });

    $(document).on('click',
        "#exportExcel",
        function () {
            $("#dtExcelButton").trigger("click");
        });
    $(document).on('click',
        "#exportPrint",
        function () {
            $("#dtPrintButton").trigger("click");
        });
    
});
