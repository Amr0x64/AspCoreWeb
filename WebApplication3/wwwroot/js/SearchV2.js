let allAddress = JSON.parse(document.querySelector("#adressStatment").innerHTML);
/*let streetNumList = JSON.parse(document.querySelector("#streetNumber").innerHTML);*/

let checkFirst = true;
let inputAddress = '';
let addressList = [];
let resultAddressInput = '';
let fulladdress = '';
let select2 = $('.select2');

$(document).ready(function () { // Инициализация
    $('.select2').select2({
        placeholder: "Введите адрес",
        data: addressList,
        closeOnSelect: false,
        tokenSeparators: [',', ' ']
    }).select2('open');
});

function event() {
    console.log("ЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫ");
    /*select2 = $('.select2');*/
        $(document).ready(function () { // Запрет закрытия
            $('.select2').on('select2:closing', function () {
                $('.select2').select2('open');
            });
        });
    
    $(document).ready(function () { // Возвращает обьект выбора
        $('.select2').on('select2:select', function (e) {
            inputAddress.value = e.params.data.text;
        });
    });

    $(document).ready(function () {
        $('.select2-search__field').focus(function () {
            inputAddress = document.getElementsByClassName('select2-search__field');
            inputAddress = inputAddress[0];
            try {
                inputAddress.oninput = search;
            }
            catch(err) {

            }
        });
    });
}

event();
//select2.on("change", function (e) {
//    select2.select2("open");
//});

let tempParent = "";
function search() {
    console.log("Работает oninput");
    if (inputAddress.value == "") {          //empty input
        console.log("Пустой инпкт");
        addressList = [];
        resultAddressInput = "";
        checkFirst = true;

        select2.select2({
            data: addressList,
            closeOnSelect: false,
            tokenSeparators: [',', ' ']
        }).select2("open").trigger('change');

        event();
    }
    else {
        resultAddressInput = inputAddress.value.split(" ");
        resultAddressInput = resultAddressInput[resultAddressInput.length - 1];
        if (checkFirst) {
            for (let address in allAddress) {
                if ((allAddress[address].level == 5 || allAddress[address].level == 8 || allAddress[address].level == 7) &&
                    (allAddress[address].address_name.toLowerCase().indexOf(resultAddressInput.toLowerCase()) == 0)) {

                    //        //for (let parent in allAddress) {
                    //        //    if (allAddress[parent].fias_guid == allAddress[address].parent_id) {
                    //        //        tempParent = `${allAddress[parent].short_type_name} - ${allAddress[parent].address_name}`;
                    //        //        break;
                    //        //    }
                    //        //}

                    addressList.push({ "id": 1, "level": allAddress[address].level, "text": `${tempParent}${allAddress[address].short_type_name} - ${allAddress[address].address_name} `});
                }
            }
            if (addressList.length != 0) {
                addressList.sort((prev, next) => next.level - prev.level);
                //addressList.sort(function (a, b) {
                //    let atext = a.text.toLowerCase(),
                //        btext = b.text.toLowerCase();
                //    if (atext < btext) return -1;
                //    else if (atext > btext) return 1;
                //});
                select2.select2({
                    data: addressList,
                    closeOnSelect: false,
                    tokenSeparators: [',', ' ']
                }).select2("open").trigger('change');   
                console.log(select2);
                event();
                checkFirst = false;
            }
        }
        else if (resultAddressInput == "") {   //поиск доч-их элементов
            /*searchChild();*/
            console.log("Мортра аутист");
        }
        else {
            console.log("Паджик пуджик");
        }
    }
}

function searchChild() {
    /*$('.select2').val(null).trigger('change');*/
}