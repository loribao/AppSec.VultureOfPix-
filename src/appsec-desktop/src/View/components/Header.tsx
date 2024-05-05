import logo from "./assets/img/logo.svg"
import "./assets/css/header.css"
export function Header() {
    return <header className="headerContainer">
    <a className="logo" href="#"
        ><img src={logo} alt=""
    /></a>
    <nav className="navContainer">
        <div className="avatarContainer" id="avatarDropdown">
            <div className="avatar"></div>
            <div className="dropDownContent">
                <a href="/myProfile.html">Meu Perfil</a>
                <a href="/forgotPassword.html">Alterar Senha</a>
                <a href="/index.html">Sair</a>
            </div>
        </div>
    </nav>
</header>
}
