import {postVerify} from "./Api/Api.js";

let FormularioVerify = document.querySelector("#FormVerify");

FormularioVerify.addEventListener('submit', (e) => {
    e.preventDefault();
    let data = Object.fromEntries(new FormData(e.target));
    let accion = e.submitter.dataset.accion

    if (accion === "Verify"){
        
        postVerify(data);
    }
});