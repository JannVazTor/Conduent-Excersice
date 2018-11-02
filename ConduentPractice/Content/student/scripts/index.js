"use strict";

$(function () {
    init();

    function init() {
        var lineChartData = app.studentControls.studentLineChartDataSet;
        var studentBarChartData = app.studentControls.studentBarChartDataSet;

        drawLineChart(lineChartData);
        drawBarChart(studentBarChartData);
    }

    $('#studentExcelFile').on('change', function () {
        $('#studentForm').trigger('submit');
    });

    function drawBarChart(data) {
        var ctx = document.getElementById("studentBarChartSummary").getContext('2d');
        var myChart = new Chart(ctx, {
            type: 'bar',
            data: data,
            options: {
                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero: true
                        }
                    }]
                }
            }
        });
    }

    function drawLineChart(data) {
        var ctx = document.getElementById("groupLineChartSummary").getContext('2d');
        var myChart = new Chart(ctx, {
            type: 'radar',
            data: data,
            options: {
                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero: true
                        }
                    }]
                }
            }
        });
    }
});
