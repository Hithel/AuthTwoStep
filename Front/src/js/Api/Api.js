

let url = "http://localhost:5005";
let headers = new Headers ({'Content-Type': 'application/json'});

export async function postRegister(data){

    let configuracion = {
        method: 'POST',
        headers: headers,
        body: JSON.stringify(data)
    };

    let User = await fetch(`${url}/api/User/Register`, configuracion);

    if (User.status === 201) {
        alert("El usuario se Registro con Exito!.");
    } else if (User.status === 400) {
        // Usuario ya registrado, muestra un mensaje de alerta
        alert("El usuario ya está registrado.");
    }
};

export async function postLogin(data){

    let configuracion = {
        method: 'POST',
        headers: headers,
        body: JSON.stringify(data)
    };

    let User = await fetch(`${url}/api/User/Login`, configuracion);

    if (User.status === 200) {
        alert("El Codigo de Verificacion QR se envio al correo!");
        window.location.href = "http://127.0.0.1:5500/Front/View/Verificacion.html";
    } else if (User.status === 400) {
        alert("El usuario es incorrecto.");
    }
};

export async function postVerify(data){

    let configuracion = {
        method: 'POST',
        headers: headers,
        body: JSON.stringify(data)
    };

    let User = await fetch(`${url}/api/User/VerifyCode`, configuracion);

    if (User.status === 200) {
        window.location.href = "http://127.0.0.1:5500/Front/View/welcome.html";
    } else if (User.status === 400) {
        alert("El usuario o el Code son incorrectas.");
    }
};