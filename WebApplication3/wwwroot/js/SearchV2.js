"use strict"
let allAddress = JSON.parse(document.querySelector("#adressStatment").innerHTML);
//let streetNumList = JSON.parse(document.querySelector("#streetNumber").innerHTML);

let checkFirst = true;
let inputAddress = '';
let addressList = [];
let resultAddressInput = '';
let fulladdress = '';
let addressNameList = [];

function event() {
    $('.selectpill').on('select2:closing', function () {
        $('.selectpill').select2('open');
    });

    $(document).ready(function () { // Возвращает обьект выбора
        $('.selectpill').on('select2:select', function (e) {
            inputAddress.value = e.params.data.text;
            searchChild();
        });
    });
}
let tempParent = {};
let value = "";

$(".selectpill").select2({
    data: [],
    closeOnSelect: false,
    tokenSeparators: [',', ' '],
    placeholder: "Ы"
}).select2("open");

inputAddress = document.getElementsByClassName('select2-search__field');
inputAddress = inputAddress[0];
inputAddress.oninput = search;
event();

let roll = {};

function search() {
    console.log("Onimput");
    if (inputAddress.value == "") {          //empty input
        addressList = [];
        addressNameList = [];
        resultAddressInput = "";
        checkFirst = true;
        fulladdress = "";
        //$(".selectpill").select2({
        //    data: addressList,
        //    closeOnSelect: false,
        //    tokenSeparators: [',', ' '],
        //    placeholder: "Sin SOBAKI"
        //}).select2("open").trigger('change');

        roll = document.getElementById("select2-addressh-results");
        roll.innerHTML = "";
        
        inputAddress = document.getElementsByClassName('select2-search__field');
        inputAddress = inputAddress[0];
        inputAddress.oninput = search;
    }
    else {
        resultAddressInput = inputAddress.value.split(" ");
        resultAddressInput = resultAddressInput[resultAddressInput.length - 1];
        if (checkFirst) {
            for (let address in allAddress) {
                if ((allAddress[address].level == 5 || allAddress[address].level == 8 || allAddress[address].level == 7) &&
                    (allAddress[address].address_name.toLowerCase().indexOf(resultAddressInput.toLowerCase()) == 0)) {

                    tempParent = allAddress.find(p => p.fias_guid == allAddress[address].parent_id);    
                    addressList.push({
                        id: 1, level: allAddress[address].level, text: `${tempParent.short_type_name} - ${tempParent.address_name}, ${allAddress[address].short_type_name} - ${allAddress[address].address_name} `
                    });
                    addressNameList.push(allAddress[address]);
                }
            }
            console.log(addressNameList);
            if (addressList.length != 0) {  
                addressList.sort((prev, next) => next.level - prev.level);
                $('.selectpill').select2('destroy');

                $(".selectpill").select2({
                    data: addressList,
                    closeOnSelect: false,
                    tokenSeparators: [',', ' '],    
                    placeholder: "Начало"
                }).select2("open");
                        
                checkFirst = false;
                value = inputAddress.value;
                inputAddress = document.getElementsByClassName('select2-search__field');
                inputAddress = inputAddress[0];
                inputAddress.value = value;
                inputAddress.oninput = search;
                event();
            }
        }
        else if (resultAddressInput == "") {   //поиск доч-их элементов
            searchChild();
        }
    }
}

function searchChild() {
    let result = "";
    let tempAddress = {};
    for (let i = inputAddress.value.length - 1; i >= 0; i--) {
        if ((inputAddress.value[i] == "-") && (inputAddress.value[i + 1]) == " ") {
            break;
        }
        else {      
            result += inputAddress.value[i];
        }
    }
    result = result.slice(1, result.length - 1); // изменить
    result = result.split('').reverse().join('');
    console.log(result);

    tempAddress = addressNameList.find(p => p.address_name == result);
    if (tempAddress) {
        fulladdress += inputAddress.value;
        addressNameList = [];
        addressList = [];
        for (let address in allAddress) {
            if (allAddress[address].parent_id == tempAddress.fias_guid) {
                addressList.push({
                    id: 1, level: allAddress[address].level, text: `${fulladdress}, ${allAddress[address].short_type_name} - ${allAddress[address].address_name} `
                });
                addressNameList.push(allAddress[address]);
            }
        }
        
        if (addressList.length != 0) {
            addressList.sort((prev, next) => next.level - prev.level);
            console.log(addressList);

            //roll = document.getElementById("select2-addressh-results");
            //roll.innerHTML = addressStr;

            $(".selectpill").select2("destroy");

            $(".selectpill").select2({
                data: addressList,
                closeOnSelect: false,
                tokenSeparators: [',', ' '],
                placeholder: "середина"
            }).select2("open");
                                                                                                                                                                                                    
            value = inputAddress.value;
            inputAddress = document.getElementsByClassName('select2-search__field');
            inputAddress = inputAddress[0];
            inputAddress.value = value;
            inputAddress.oninput = search;
            event();
        }
    }
    //Поиск домов
    else {
       
    }
}
/////////////////////////

//let user = {
//    name: "Собака на мортре",
//    sayHy() {
//        alert(this.name);
//    }
//};
//user.sayHy();

//let emp = {};

//for (let item in user) {
//    emp[item] = user[item];
//}
///*console.log(emp);*/

//let use1 = {
//    name: "Собака на мортре"
//};

//let emp2 = {};

//console.log(Object.assign(emp2, use1));