function GetLabels(data) {

    const categoryLabels = [];
    const numberOfCategories = data.length;
    var index = 0;

    while (index < numberOfCategories) {
        categoryLabels.push(data[index].listOfCategories);
        index = index + 1;
    }

    return categoryLabels;
}

function GetDays(data) {

    const dayLabels = [];
    const numberOfDays = data.length;
    var index = 0;

    while (index < numberOfDays) {
        dayLabels.push(data[index].listOfDays);
        index = index + 1;
    }

    return dayLabels;
}

function GetMonths(data) {

    const monthLabels = [];
    const numberOfMonths = data.monthNames.length;
    var index = 0;

    while (index < numberOfMonths) {
        monthLabels.push(data.monthNames[index]);
        index = index + 1;

    }

    return monthLabels;
}

function GetExpensesValues(data) {

    const expensesValues = [];
    const numberOfValues = data.expenseValues.length;
    var index = 0;

    while (index < numberOfValues) {
        expensesValues.push(data.expenseValues[index].toFixed(2));
        index = index + 1;
    }

    return expensesValues;
}

function GetBudgetsValues(data) {

    const budgetsValues = [];
    const numberOfValues = data.budgetValues.length;
    var index = 0;

    while (index < numberOfValues) {
        budgetsValues.push(data.budgetValues[index].toFixed(2));
        index = index + 1;
    }

    return budgetsValues;
}    
