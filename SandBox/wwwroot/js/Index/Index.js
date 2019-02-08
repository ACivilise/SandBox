'use strict';

var fonctions;

$(document).ready(function () {
    fonctions = $("#fonctions");
    $("#startTrainingBtn").click(StartTraining);
    $("#firstPageBtn").click(Predict);
});

function StartTraining() {
    $("#TrainingSpinner").removeClass("d-none");
    var url = fonctions.data('url-train-model');
    $.get(url)
        .done(function (result) {
            $("#TrainingSpinner").addClass("d-none");
            if (result) {
                $("#MsgOk").removeClass("d-none");
                $("#MsgNotOk").addClass("d-none");
            }
            else {
                $("#MsgNotOk").removeClass("d-none");
                $("#MsgOk").addClass("d-none");
            }
        })
        .fail(function (jqXHR, textStatus) {
            $("#TrainingSpinner").addClass("d-none");
            console.log(url + " erreur : " + textStatus);
        });
}

function Predict() {
    $("#firstPageSpinner").removeClass("d-none");
    //$("#MsgNotOk").addClass("d-none");
    //$("#MsgOk").addClass("d-none");
    var url = fonctions.data('url-predict');
    $.get(url)
        .done(function (partialView) {
            $("#firstPageSpinner").addClass("d-none");
            $("#firstPageContent").html(partialView);
        })
        .fail(function (jqXHR, textStatus) {
            $("#firstPageSpinner").addClass("d-none");
            console.log(url + " erreur : " + textStatus);
        });
}