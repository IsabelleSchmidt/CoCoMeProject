// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let cashboxInput = "";
let receiveCashPayment = false;
let receiveObjectId = false;

const okButton = document.querySelector("#input-ok");
const itemPlusOneButton = document.querySelector("#item-plus-one");
const itemByIdButton = document.querySelector("#item-by-id");
const cashPaymentButton = document.querySelector("#cash-payment");
const cardPaymentButton = document.querySelector("#card-payment");
const newSaleButton = document.querySelector("#new-sale");
const finishSaleButton = document.querySelector("#sale-finished");
const deleteItemButton = document.querySelector("#delete-current-item");
const stopExpressButton = document.querySelector("#stop-express-checkout");

document.onkeydown = function (e) {
    //optional
    return false;
}

okButton.addEventListener('click', event => {
    //ok
    if (cashboxInput.length > 0) {
        if (receiveObjectId) {
            //in controller: get object, add object to list
            receiveObjectId = false;
        } else if (receiveCashPayment) {
            let amount = parseFloat(cashBoxInput);
            //give amount to Controller, there calculate cashback, display 
            receiveCashPayment = false;
        }
        disableInputCashBox();
    }
   

});
itemPlusOneButton.addEventListener('click', event => {
    //last entered item +1 (needed here?)
});
itemByIdButton.addEventListener('click', event => {
    //enable input, after search for item
    
    itemByIdButton.disabled = true;
    okButton.disabled = false;
    finishSaleButton.disabled = true;
    itemPlusOneButton.disabled = true;
    deleteItemButton.disabled = true;
   
    enableInputCashBox();
    receiveObjectId = true;
});
cashPaymentButton.addEventListener('click', event => {
    //enable input, in controller get full price, calculate cashback
    cardPaymentButton.disabled = true;
    cashPaymentButton.disabled = true;
    okButton.disabled = false;
    enableInputCashBox();
    receiveCashPayment = true;
   
    
});
cardPaymentButton.addEventListener('click', event => {
    //controller: cardReader
    cardPaymentButton.disabled = true;
    cashPaymentButton.disabled = true;

});
newSaleButton.addEventListener('click', event => {
    //conroller: clear old sale, activate scanner, et
    newSaleButton.disabled = true;
    finishSaleButton.disabled = false;
    itemByIdButton.disabled = false;
    itemPlusOneButton.disabled = false;
    deleteItemButton.disabled = false;

   
});
finishSaleButton.addEventListener('click', event => {
    //controller: deactivate other input, activate card, cash buttons
    if (finishSaleActive) {
        finishSaleButton.disabled = true;
        itemByIdButton.disabled = true;
        itemPlusOneButton.disabled = true;
        deleteItemButton.disabled = true;
        cardPaymentButton.disabled = false;
        cashPaymentButton.disabled = false;
    }
});
deleteItemButton.addEventListener('click', event => {
    //controller: delete last item in list
    if (deleteItemActive) {
        //and if list>0
    }
});
stopExpressButton.addEventListener('click', event => {
    //controller: change to nomal checkout mode, activate paying by credit card
    event.preventDefault();
    
    stopExpressButton.disabled = true;
    $.ajax({

        url: $(this).attr("href"), // comma here instead of semicolon   
        success: function () {
            alert("Value Added");  // or any other indication if you want to show
        }

    });
    
});

function enableInputCashBox() {

    document.querySelector("#input-cashbox").disabled = false;
    document.querySelector("#input-cashbox").select();
}
function disableInputCashBox() {
    document.querySelector("#input-cashbox").disabled = true;
}

function addToInputField(newInput) {

    inputField = document.querySelector("#input-cashbox");
    if (receiveObjectId || receiveCashPayment) {
        cashboxInput += newInput;
        console.log("Input: " + cashboxInput);
        inputField.value = cashboxInput;
    }
    
}

function activateKeys() {

}


function removeLastInput() {
    if (cashboxInput.length > 0) {
        cashboxInput = cashboxInput.substring(0, cashboxInput.length - 1);
        document.querySelector("#input-cashbox").value = cashboxInput;
    }
    
}





