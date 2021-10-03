let allAddress = JSON.parse(document.querySelector("#adressStatment").innerHTML);
//let streetNumList = JSON.parse(document.querySelector("#streetNumber").innerHTML);

let checkFirst = true;
let inputAddress = '';
let addressList = [];
let resultAddressInput = '';
let fulladdress = '';
/*let select2 = $('.select2');*/

$('.select2').select2({
    placeholder: "Введите адрес",
    data: addressList,
    closeOnSelect: false,
    tokenSeparators: [',', ' ']
}).select2('open');

function event() {
    console.log("ЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫ");
    /*select2 = $('.select2');*/
    $('.select2').on('select2:closing', function () {
        $('.select2').select2('open');
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
            inputAddress.oninput = search;
        });
    });
}

event();
let tempParent = {};

function search() {
    if (inputAddress.value == "") {          //empty input
        console.log("Пустой инпкт");
        addressList = [];
        resultAddressInput = "";
        checkFirst = true;

        $(".select2").select2({
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
                    tempParent = allAddress.find(p => p.fias_guid == allAddress[address].parent_id);
                    addressList.push({ "id": 1, "level": allAddress[address].level, "text": `${tempParent.address_name}, ${allAddress[address].short_type_name} - ${allAddress[address].address_name} ` });
                }
            }
            if (addressList.length != 0) {
                addressList.sort((prev, next) => next.level - prev.level);
                $(".select2").select2({
                    data: addressList,
                    closeOnSelect: false,
                    tokenSeparators: [',', ' ']
                }).select2("open").trigger('change');
                console.log(inputAddress.value);
                event();
                console.log(inputAddress.value);
                checkFirst = false;
            }
        }
        else if (resultAddressInput == "") {   //поиск доч-их элементов
            /*searchChild();*/
            console.log("Мортра аутист");
        }
    }
}

function searchChild() {
    /*$('.select2').val(null).trigger('change');*/
}

//let arr = [{ a: 1 }, { a: 1 }, { a: 1 }];
//let mapped = arr.filter(el => el.a == 1);
//console.log(mapped);

