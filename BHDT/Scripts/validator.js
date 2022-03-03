
function Validator(form, childForm, min, str) {
   
    // handle when submit form
    formEl.onsubmit = (e) => {
        e.preventDefault();

        // check all forms before submitting
        //for (let inpEl of inpEls) {
        //    if (inpEl.value) {
        //        ruleChecking(inpEl);
        //    } else {
        //        messageAdding(inpEl, str || 'Vui lòng nhập trường này', str || 'red');
        //    }
        //}
    }
}

