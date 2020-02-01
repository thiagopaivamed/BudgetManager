function GetProgressBarBudgetsData() {

    var month = $(".monthspiedropdown").val();
    var year = $(".yearspiedropdown").val();

    $.ajax({
        method: 'POST',
        url: '/Dashboard/GetBudgetsData',
        data: { month: month, year: year },
        success: function (data) {           
            var index = 0;
            var sumOfValues = 0;

            while (index < data.length) {
                sumOfValues = sumOfValues + data[index].listOfValues;
                index = index + 1;
            }

            index = 0;

            $("div.BudgetsAndExpensesProgessBars div.divProgressBars").remove();
            $("div.BudgetsAndExpensesProgessBars").append("<div class='divProgressBars'></div>");

            while (index < data.length) {

                $("div.divProgressBars").append(BuildBudgetProgressBar(data, index, sumOfValues));
                $("div.divProgressBars").append('<br/>');
                index = index + 1;
            }
        }
    });
}

function BuildBudgetProgressBar(data, index, sumOfValues) {

    var width = (data[index].listOfValues / sumOfValues) * 100;
    var valueNow = data[index].listOfValues;
    var valueMin = 0;
    var valueMax = sumOfValues;
    var bgProgressBar = ["bg-primary", "bg-secondary", "bg-success", "bg-danger", "bg-warning"];
    var span = "<span class='badge badge-pill badge-light'>Day " + data[index].listOfDays + "</span>";
    var div = span + "<div class='progress'><div class='progress-bar " + bgProgressBar[Math.floor(Math.random() * 5)] + "' role='progressbar' style='width: " + width + " % ' aria-valuenow=" + valueNow.toFixed(2) + "aria-valuemin=" + valueMin + "aria-valuemax=" + valueMax + ">" + valueNow.toFixed(2) + "</div></div>";

    return div;

}

function BuildExpenseProgressBar(data, index, sumOfValues) {

    var width = (data[index].listOfValues / sumOfValues) * 100;
    var valueNow = data[index].listOfValues;
    var valueMin = 0;
    var valueMax = sumOfValues;
    var bgProgressBar = ["bg-primary", "bg-secondary", "bg-success", "bg-danger", "bg-warning"];
    var span = "<span class='badge badge-pill badge-light'>" + data[index].listOfCategories + "</span>";
    var div = span + "<div class='progress'><div class='progress-bar " + bgProgressBar[Math.floor(Math.random() * 5)] + "' role='progressbar' style='width: " + width + " % ' aria-valuenow=" + valueNow.toFixed(2) + "aria-valuemin=" + valueMin + "aria-valuemax=" + valueMax + ">" + valueNow.toFixed(2) + "</div></div>";

    return div;

}

$(".expenses").on('click', function () {

    var month = $(".monthspiedropdown").val();
    var year = $(".yearspiedropdown").val();    


    $.ajax({
        method: 'POST',
        url: '/Dashboard/GetExpensesData',
        data: { month: month, year: year },
        success: function (data) {

            var index = 0;
            var sumOfValues = 0;

            while (index < data.length) {
                sumOfValues = sumOfValues + data[index].listOfValues;
                index = index + 1;
            }

            index = 0;

            $("div.BudgetsAndExpensesProgessBars div.divProgressBars").remove();
            $("div.BudgetsAndExpensesProgessBars").append("<div class='divProgressBars'></div>");

            while (index < data.length) {

                $("div.divProgressBars").append(BuildExpenseProgressBar(data, index, sumOfValues.toFixed(2)));
                $("div.divProgressBars").append('<br/>');
                index = index + 1;
            }
        }
    });
});

$(".budgets").on('click', function () {

    GetProgressBarBudgetsData();
});

$(".monthspiedropdown, .yearspiedropdown").on('change', function () {

    var month = $(".monthspiedropdown").val();
    var year = $(".yearspiedropdown").val();  

    $("canvas#BudgetsAndExpensesPieChart").remove();
    $("div.PieChart").append('<canvas id="BudgetsAndExpensesPieChart" style="width:200px;height:200px;"></canvas>');

    $.ajax({
        method: 'POST',
        url: '/Dashboard/BudgetsAndExpensesData',
        data: { month: month, year: year },
        success: function (data) {
            BuildPieGraphic(data);
        }
    });

    $.ajax({
        method: 'POST',
        url: '/Dashboard/GetBudgetsData',
        data: { month: month, year: year },
        success: function (data) {
            var index = 0;
            var sumOfValues = 0;

            while (index < data.length) {
                sumOfValues = sumOfValues + data[index].listOfValues;
                index = index + 1;
            }

            index = 0;

            $("div.BudgetsAndExpensesProgessBars div.divProgressBars").remove();
            $("div.BudgetsAndExpensesProgessBars").append("<div class='divProgressBars'></div>");

            while (index < data.length) {

                $("div.divProgressBars").append(BuildBudgetProgressBar(data, index, sumOfValues.toFixed(2)));
                $("div.divProgressBars").append('<br/>');
                index = index + 1;
            }
        }
    });
});