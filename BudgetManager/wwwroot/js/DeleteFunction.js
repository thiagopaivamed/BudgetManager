function DeleteData(rowId, controller) {

    $('.modal').modal('show');
    var url = "/" + controller + "/Delete";

    if (controller === "Categories") {
        $(".typeofdata").text("category");
    }

    else if (controller === "Expenses") {
        $(".typeofdata").text("expense");        
    }

    else {
        $(".typeofdata").text("budget");
    }    

    $('.btnYes').on('click', function () {
        $.ajax({
            method: "POST",
            url: url,
            data: { rowId: rowId },
            success: function () {
                window.location = self.location;
            }
        });
    });
}