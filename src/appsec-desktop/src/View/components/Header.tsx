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
                    className="avatarContainer"
                    id="avatarDropdown"
                    onClick={toggleDropdown}
                >
                    <div className="avatar"></div>
                    <div
                        className={`dropDownContentHeader ${
                            dropdownVisible ? "activeDropDownContentHeader" : ""
                        }`}
                    >
                        <a href="#">Meu Perfil</a>
                        <a href="#">Alterar Senha</a>
                        <a href="#">Sair</a>
                    </div>
                </div>
            </nav>
        </header>
    );
}
