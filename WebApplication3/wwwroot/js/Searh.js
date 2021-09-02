let serAddress = document.querySelector("#adressStatment").innerHTML;
let json = JSON.parse(serAddress);
let strAdress = '';
const dataList = document.getElementById('adressList');
let statmentList = [];
let tempStatmentList = [];
startAddress(8);

input.oninput = function () {
    if (input.value == "") {
        startAddress(8);
    }
    
    else if ((input.value[input.value.length - 1]) == " ") {
        let result = "";
        for (let i = input.value.length - 1; i >= 0; i--) {
            if ((input.value[i] == "-") && (input.value[i + 1]) == " ") {
                break; 
            }
            else { 
                result += input.value[i];
            }
        }
        result = result.slice(1, result.length - 1);
        result = result.split('').reverse().join('');
        console.log(result);
        //Изменить
        for (let key in statmentList) {
            if (statmentList[key].address_name == result) {
                tempStatmentList = statmentList.slice(0);
                statmentList = [];
                for (let child in json) {
                    if (json[child].parent_id == tempStatmentList[key].fias_guid) {
                        statmentList.push(json[child]);
                        strAdress += `
                            <option value="${tempStatmentList[key].short_type_name} - ${tempStatmentList[key].address_name}, ${json[child].short_type_name} - ${json[child].address_name},">${json[child].address_name}</option ><br/>
                        `;
                    }
                }
                if (statmentList[0] != undefined) {
                    dataList.innerHTML = "";
                    dataList.innerHTML = strAdress;
                    strAdress = "";
                }
                break;
            }         
        }
    }
};

function startAddress(i) {
    for (let key in json) {
        if (json[key].level == i) {
            statmentList.push(json[key]);
            strAdress += `
            <option>${json[key].short_type_name} - ${json[key].address_name}</option><br/>
                `;
        }
    }
    dataList.innerHTML = strAdress;
    strAdress = "";
}