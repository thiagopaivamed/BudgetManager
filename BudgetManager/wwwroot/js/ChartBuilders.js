function GetPieChartData() {
    
    var month = $(".monthspiedropdown").val();
    var year = $(".yearspiedropdown").val();    

    $.ajax({
        method: 'POST',
        url: '/Dashboard/BudgetsAndExpensesData',
        data: { month: month, year: year },
        success: function (data) {
            BuildPieGraphic(data);
        }
    });
}

function BuildPieGraphic(data) {
    new Chart(document.getElementById("BudgetsAndExpensesPieChart"), {
        type: 'pie',
        data: {
            labels: ["Expenses", "Budgets"],
            datasets: [{
                label: 'Expenses and Budgets',
                data: [data.sumOfExpenses, data.sumOfBudgets],
                backgroundColor: ["#c0392b", "#2980b9"],
                fill: false,
                spanGaps: false
            }]
        },
        options: {
            responsive: false,

            legend: {
                position: "bottom",
                labels: {
                    usePointStyle: true
                }

            }
        }
    });
}

function GetBarChartBudgetsAndExpensesByYear() {

   
    var year = $(".yearsdropdownbar").val();

    $.ajax({
        method: "POST",
        url: "/Dashboard/GetBudgetsAndExpensesByYear",
        data: { year: year },
        success: function (data) {
            BuildBarGraphic(data);
        }
    });
}

function BuildBarGraphic(data) {
    new Chart(document.getElementById("BudgetsAndExpensesByYearBarChart"), {
        type: 'bar',
        data: {
            labels: GetMonths(data),
            datasets: [{
                label: 'Budget',
                data: GetBudgetsValues(data),
                backgroundColor: "#2980b9",
                hoverBackgroundColor: "#3498db",
                fill: false,
                spanGaps: false
            },

            {
                label: 'Expense',
                data: GetExpensesValues(data),
                backgroundColor: "#c0392b",
                hoverBackgroundColor: "#c0392b",
                fill: false,
                spanGaps: false
            }]
        },
        options: {
            responsive: true,
            scales: {
                xAxes: [{
                    barPercentage: 0.9
                }]
            }
        }
    });
}

$(".yearsdropdownbar").on('change', function () {

    var year = $(".yearsdropdownbar").val();

    $("canvas#BudgetsAndExpensesByYearBarChart").remove();
    $("div.BudgetsAndExpensesByYear").append('<canvas id="BudgetsAndExpensesByYearBarChart"></canvas>');

    $.ajax({
        method: 'POST',
        url: '/Dashboard/GetBudgetsAndExpensesByYear',
        data: { year: year },
        success: function (data) {
            BuildBarGraphic(data);
        }
    });
});