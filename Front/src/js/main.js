import {postRegister,postLogin} from "./Api/Api.js";

let buttonLogin= document.getElementById("button-login");
let buttomSignup= document.getElementById("buttom-sign-up");

let formRegister= document.querySelector(".register");
let formLoggin= document.querySelector(".loggin");

let FormularioRegister = document.querySelector("#FormRegister");
let FormularioLoggin = document.querySelector("#FormLogin");



buttonLogin.addEventListener("click", e=>{

    formRegister.classList.add("hide");
    formLoggin.classList.remove("hide");

});

buttomSignup.addEventListener("click", e=>{

    formLoggin.classList.add("hide");
    formRegister.classList.remove("hide");
    
});

FormularioRegister.addEventListener('submit', (e) => {
    e.preventDefault();
    let data = Object.fromEntries(new FormData(e.target));
    let accion = e.submitter.dataset.accion

    if (accion === "register"){
        postRegister(data);
    }
    FormularioRegister.reset();

});

FormularioLoggin.addEventListener('submit', (e) => {
    e.preventDefault();
    let data = Object.fromEntries(new FormData(e.target));
    let accion = e.submitter.dataset.accion

    if (accion === "SingIn"){
        postLogin(data);
    }

    FormularioLoggin.reset();

});


