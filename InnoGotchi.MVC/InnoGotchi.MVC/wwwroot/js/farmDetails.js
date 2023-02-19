jQuery(document).ready(function () {

	$('#body-select').change(function () {
        var bodyType = this.value;
        var eyes = document.getElementById("eyes");
        var nose = document.getElementById("nose");
        var mouth = document.getElementById("mouth");
        var body = document.getElementById("body");
        if (bodyType == 0) {
            body.src = '';
            return;
        }
        body.src = `https://localhost:7208/images/bodies/body${bodyType}.svg`;
        console.log(bodyType);
        if (bodyType == 1) {
            body.style.maxWidth = '90%';
            body.style.marginTop = '0%';

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
            body.style.maxWidth = '90%';
            body.style.marginTop = '0%';

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
            body.style.maxWidth = '90%';
            body.style.marginTop = '0%';

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
            body.style.maxWidth = '90%';
            body.style.marginTop = '0%';

            eyes.style.maxWidth = '12%';
            eyes.style.marginLeft = "38%";
            eyes.style.marginTop = '6%';

            nose.style.maxWidth = '3%';
            nose.style.marginLeft = '43%';
            nose.style.marginTop = '12%';

            mouth.style.maxWidth = '13%';
            mouth.style.marginLeft = "38%";
            mouth.style.marginTop = '11%';
        }
        else if (bodyType == 5) {
            body.style.maxWidth = '79%';
            body.style.marginTop = '-20%';

            eyes.style.maxWidth = '13%';
            eyes.style.marginLeft = "37%";
            eyes.style.marginTop = '4%';

            nose.style.maxWidth = '3%';
            nose.style.marginLeft = "56%";
            nose.style.marginTop = '12%';

            mouth.style.maxWidth = '18%';
            mouth.style.marginLeft = "44%";
            mouth.style.marginTop = '11%';
        }
    });
    $('#eye-select').change(function () {
        var eyeType = this.value;
        var eyes = document.getElementById("eyes");
        if (eyeType == 0) {
            eyes.src = '';
            return;
        }
        eyes.src = `https://localhost:7208/images/eyes/eyes${eyeType}.svg`

    });
    $('#nose-select').change(function () {
        var noseType = this.value;
        var nose = document.getElementById("nose");
        if (noseType == 0) {
            nose.src = '';
            return;
        }
        nose.src = `https://localhost:7208/images/noses/nose${noseType}.svg`

    });
    $('#mouth-select').change(function () {
        var mouthType = this.value;
        var mouth = document.getElementById("mouth");
        if (mouthType == 0) {
            mouth.src = '';
            return;
        }
        mouth.src = `https://localhost:7208/images/mouths/mouth${mouthType}.svg`

    });
});