var ctx6 = document.getElementById("pieChart6");
let alive = Number(ctx6.dataset.alive);
let dead = Number(ctx6.dataset.dead);
var pieChart6 = new Chart(ctx6, {
    type: 'pie',
    options: {
        rotation: -20,
        cutoutPercentage: 0,
        animation: {
            animateScale: true,
        },
        legend: {
            position: 'left',
            borderAlign: 'inner',
            labels: {
                boxWidth: 10,
                fontStyle: 'italic',
                fontColor: '#aaa',
                usePointStyle: true,
            }
        },
    },
    data: {
        labels: [
            "Alive",
            "Dead",
        ],
        datasets: [
            {
                data: [alive, dead],
                borderWidth: 2,
                backgroundColor: [
                    'rgba(70, 215, 212, 0.2)',
                    "rgba(245, 225, 50, 0.2)",
                ],
                borderColor: [
                    '#46d8d5',
                    "#f5e132",
                ],
                hoverBackgroundColor: [
                    '#46d8d5',
                    "#f5e132",
                ]
            }]
    }
});