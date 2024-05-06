import React, { useState } from "react";
import logo from "../assets/img/Vulture_Art.svg";
import "./Header.css";

export function Header() {
    const [dropdownVisible, setDropdownVisible] = useState(false);

    const toggleDropdown = () => {
        setDropdownVisible(!dropdownVisible);
    };

    return (
        <header className="headerContainer">
            <a className="logo" href="#">
                <img src={logo} alt="" />
            </a>
            <nav className="navContainer">
                <div
                    className={`avatarContainer ${
                        dropdownVisible ? "active" : ""
                    }`}
                    id="avatarDropdown"
                    onClick={toggleDropdown}
                >
                    <div className="avatar"></div>
                    <div className="dropDownContent">
                        <a href="/myProfile.html">Meu Perfil</a>
                        <a href="/forgotPassword.html">Alterar Senha</a>
                        <a href="/index.html">Sair</a>
                    </div>
                </div>
            </nav>
        </header>
    );
}
