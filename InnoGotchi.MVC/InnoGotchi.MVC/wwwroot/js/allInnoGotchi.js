var eyes = document.getElementsByName("eyes");
var nose = document.getElementsByName("nose");
var mouth = document.getElementsByName("mouth");
var body = document.getElementsByName("body");
for (let i = 0; i < body.length; i++) {
    var bodyType = Number(body[i].dataset.type);
    if (bodyType == 1) {
        eyes[i].style.maxWidth = '15%';
        eyes[i].style.marginLeft = "31%";
        eyes[i].style.marginTop = '10%';

        nose[i].style.maxWidth = '5%';
        nose[i].style.marginLeft = "36%";
        nose[i].style.marginTop = '19%';

        mouth[i].style.maxWidth = '14%';
        mouth[i].style.marginLeft = "32%";
        mouth[i].style.marginTop = '19%';
    }
    else if (bodyType == 2) {
        eyes[i].style.maxWidth = '20%';
        eyes[i].style.marginLeft = "32%";
        eyes[i].style.marginTop = '15%';

        nose[i].style.maxWidth = '8%';
        nose[i].style.marginLeft = "38%";
        nose[i].style.marginTop = '26%';

        mouth[i].style.maxWidth = '19%';
        mouth[i].style.marginLeft = "33%";
        mouth[i].style.marginTop = '28%';
    }
    else if (bodyType == 3) {
        eyes[i].style.maxWidth = '15%';
        eyes[i].style.marginLeft = "35%";
        eyes[i].style.marginTop = '10%';

        nose[i].style.maxWidth = '5%';
        nose[i].style.marginLeft = "40%";
        nose[i].style.marginTop = '19%';

        mouth[i].style.maxWidth = '14%';
        mouth[i].style.marginLeft = "36%";
        mouth[i].style.marginTop = '19%';
    }
    else if (bodyType == 4) {
        eyes[i].style.maxWidth = '12%';
        eyes[i].style.marginLeft = "38%";
        eyes[i].style.marginTop = '6%';

        nose[i].style.maxWidth = '3%';
        nose[i].style.marginLeft = "43%";
        nose[i].style.marginTop = '12%';

        mouth[i].style.maxWidth = '13%';
        mouth[i].style.marginLeft = "38%";
        mouth[i].style.marginTop = '11%';
    }
    else if (bodyType == 5) {
        body[i].style.maxWidth = '80%';
        body[i].style.marginTop = '-20%';

        eyes[i].style.maxWidth = '13%';
        eyes[i].style.marginLeft = "37%";
        eyes[i].style.marginTop = '4%';

        nose[i].style.maxWidth = '3%';
        nose[i].style.marginLeft = "56%";
        nose[i].style.marginTop = '12%';

        mouth[i].style.maxWidth = '18%';
        mouth[i].style.marginLeft = "44%";
        mouth[i].style.marginTop = '11%';
    }

}
