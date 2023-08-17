
$(document).ready(function () {
    var kr = '❎';
    var nl = '⭕';
    var emp = '';

    $(document).click(function (e) {
        let elem = $(e.target);
        if (elem.html() == emp && elem.hasClass('field')) {
            elem.html(kr);
            setTimeout(doStep, 300);
        }
        else if (elem.html() == emp && elem.hasClass('field')) {
            elem.html(nl);
            setTimeout(doStep, 300);
        }
        else if (elem.html() != emp && elem.hasClass('field')) {
            alert('ячейка занята!');
        }
    });

    $(".btn-game").click(function () {
        location.reload();
    })

    function doStep() {

        //собираем данные с игрового поля
        a1 = $('#a1').html();
        a2 = $('#a2').html();
        a3 = $('#a3').html();

        b1 = $('#b1').html();
        b2 = $('#b2').html();
        b3 = $('#b3').html();

        c1 = $('#c1').html();
        c2 = $('#c2').html();
        c3 = $('#c3').html();

        //данные отправляем в функцию
        $.get('Home/GetInfo/', { a1: a1, a2: a2, a3: a3, b1: b1, b2: b2, b3: b3, c1: c1, c2: c2, c3: c3 }, function (data) {
            $('.text-danger').html(data);
            console.log(data);
            //данные полученные от getInfo записываем в игровое поле
            $("#a1").html(data[0]);
            $("#a2").html(data[1]);
            $("#a3").html(data[2]);
            $("#b1").html(data[3]);
            $("#b2").html(data[4]);
            $("#b3").html(data[5]);
            $("#c1").html(data[6]);
            $("#c2").html(data[7]);
            $("#c3").html(data[8]);
            //*******************
            if (data[9] != undefined) {
                if (data[9] == "Победа ❎") {
                    setTimeout(function () { $(".winner").html("<img class='img-fluid' src='pic/you_win.jpg'>") }, 400);
                }
                else if (data[9] == "Победа ⭕") {
                    setTimeout(function () { $(".winner").html("<img class='img-fluid' src='pic/game_over.jpg'>") }, 400);
                }
                else setTimeout(function () { $(".winner").html("<img class='img-fluid' src='pic/draw.png'>") }, 400);
            }

            if (data[9] == "Победа ❎" || data[9] == "Победа ⭕") {
                $('#a1').removeClass("field");
                $('#a2').removeClass("field");
                $('#a3').removeClass("field");

                $('#b1').removeClass("field");
                $('#b2').removeClass("field");
                $('#b3').removeClass("field");

                $('#c1').removeClass("field");
                $('#c2').removeClass("field");
                $('#c3').removeClass("field");
            }
        });
        
    }

});



