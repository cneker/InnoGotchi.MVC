jQuery(document).ready(function () {
    var eyes = document.getElementById("eyes");
    var nose = document.getElementById("nose");
    var mouth = document.getElementById("mouth");
    var body = document.getElementById("body");
    var bodyType = Number(body.dataset.type);
    if (bodyType == 1) {
        eyes.style.maxWidth = '15%';
        eyes.style.marginLeft = "31%";
        eyes.style.marginTop = '10%';

        nose.style.maxWidth = '5%';
        nose.style.marginLeft = "36%";
        nose.style.marginTop = '19%';

        mouth.style.maxWidth = '14%';
        mouth.style.marginLeft = "32%";
        mouth.style.marginTop = '19%';
    }
    else if (bodyType == 2) {
        eyes.style.maxWidth = '20%';
        eyes.style.marginLeft = "32%";
        eyes.style.marginTop = '15%';

        nose.style.maxWidth = '8%';
        nose.style.marginLeft = "38%";
        nose.style.marginTop = '26%';

        mouth.style.maxWidth = '19%';
        mouth.style.marginLeft = "33%";
        mouth.style.marginTop = '28%';
    }
    else if (bodyType == 3) {
        eyes.style.maxWidth = '15%';
        eyes.style.marginLeft = "35%";
        eyes.style.marginTop = '10%';

        nose.style.maxWidth = '5%';
        nose.style.marginLeft = "40%";
        nose.style.marginTop = '19%';

        mouth.style.maxWidth = '14%';
        mouth.style.marginLeft = "36%";
        mouth.style.marginTop = '19%';
    }
    else if (bodyType == 4) {
        eyes.style.maxWidth = '12%';
        eyes.style.marginLeft = "38%";
        eyes.style.marginTop = '6%';

        nose.style.maxWidth = '3%';
        nose.style.marginLeft = "43%";
        nose.style.marginTop = '12%';

        mouth.style.maxWidth = '13%';
        mouth.style.marginLeft = "38%";
        mouth.style.marginTop = '11%';
    }
    else if (bodyType == 5) {
        body.style.maxWidth = '80%';
        body.style.marginTop = '-20%';
        body.style.marginLeft = '10%';

        eyes.style.maxWidth = '13%';
        eyes.style.marginLeft = "47%";
        eyes.style.marginTop = '4%';

        nose.style.maxWidth = '3%';
        nose.style.marginLeft = "66%";
        nose.style.marginTop = '12%';

        mouth.style.maxWidth = '18%';
        mouth.style.marginLeft = "54%";
        mouth.style.marginTop = '11%';
    }

});
