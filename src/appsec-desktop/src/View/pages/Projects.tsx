import React, { useState, useEffect, useRef } from "react";
import { Header } from "../components/Header";
import "./Projects.css"
import edit from "../assets/img/edit.svg";
import trash from "../assets/img/trash.svg";
import pull from "../assets/img/pull.svg";

const Projetos = () => {
    const [formPopOutOpen, setFormPopOutOpen] = useState(false);
    const [languageDropdownOpen, setLanguageDropdownOpen] = useState(false);

    const dropdownRef = useRef(null);

    useEffect(() => {
        function handleClickOutside(event) {
            if (
                dropdownRef.current &&
                !dropdownRef.current.contains(event.target)
            ) {
                setLanguageDropdownOpen(false);
            }
        }

        window.addEventListener("click", handleClickOutside);
        return () => window.removeEventListener("click", handleClickOutside);
    }, []);

    const toggleFormPopOut = () => {
        setFormPopOutOpen(!formPopOutOpen);
    };

    const toggleLanguageDropdown = () => {
        setLanguageDropdownOpen(!languageDropdownOpen);
    };

    const openForm = () => {
        setFormPopOutOpen(true);
    };

    const closeForm = () => {
        setFormPopOutOpen(false);
    };

    return (
        <main className="mainContainer">
            <Header />
            <article className="articleHeader">
                <h1 className="title">Meus Projetos</h1>
                <a
                    id="openForm"
                    className="newProject"
                    href="#"
                    onClick={openForm}
                >
                    Novo Projeto
                </a>
                <div
                    id="menuSidebar"
                    className={`sidebar ${formPopOutOpen ? "open" : ""}`}
                >
                    <form className="formContainer">
                        <input type="text" placeholder="Nome do Projeto" />
                        <input type="text" placeholder="Descrição do Projeto" />
                        <input type="text" placeholder="URL do Git" />
                        <input type="text" placeholder="Branch do Git" />
                        <input
                            type="text"
                            placeholder="Usuário do Repositório"
                        />
                        <input type="text" placeholder="Email do Repositório" />
                        <input type="email" placeholder="URL do Sast" />
                        <input type="email" placeholder="Usuário do Sast" />
                        <input type="email" placeholder="Senha do Sast" />
                        <input type="email" placeholder="URL do Dast" />
                        <input type="email" placeholder="Usuário do Dast" />
                        <input type="email" placeholder="Senha do Dast" />
                        <div className="dropdown" ref={dropdownRef}>
                            <button
                                className="dropbtn"
                                onClick={toggleLanguageDropdown}
                            >
                                Selecione uma Linguagem
                            </button>
                            <div
                                className={`dropdownContent ${
                                    languageDropdownOpen ? "show" : ""
                                }`}
                                id="dropdownContent"
                            >
                                <a href="#">JavaScript</a>
                                <a href="#">Python</a>
                                <a href="#">Java</a>
                                <a href="#">C++</a>
                                <a href="#">C#</a>
                                <a href="#">PHP</a>
                                <a href="#">Ruby</a>
                                <a href="#">Swift</a>
                                <a href="#">Kotlin</a>
                                <a href="#">Go (Golang)</a>
                                <a href="#">R</a>
                                <a href="#">TypeScript</a>
                                <a href="#">Scala</a>
                                <a href="#">Perl</a>
                                <a href="#">Lua</a>
                                <a href="#">Assembly</a>
                                <a href="#">Dart</a>
                                <a href="#">Rust</a>
                                <a href="#">Swift</a>
                            </div>
                            <div className="buttons">
                                <button className="submit" type="submit">
                                    Enviar Projeto
                                </button>
                                <button
                                    className="close"
                                    id="closeForm"
                                    onClick={closeForm}
                                >
                                    Fechar
                                </button>
                            </div>
                        </div>
                    </form>
                </div>
            </article>
            <article className="articleContainer">
                <article className="articleItem">
                    <h3>Projeto</h3>
                    <div className="articleLink">
                        <a href="#" onClick={toggleFormPopOut}>
                            <img src={edit} alt="" />
                        </a>
                        <a href="./">
                            <img src={pull} alt="" />
                        </a>
                        <a href="./">
                            <img src={trash} alt="" />
                        </a>
                    </div>
                </article>
                <article className="articleItem">
                    <h3>Projeto</h3>
                    <div className="articleLink">
                        <a href="#" onClick={toggleFormPopOut}>
                            <img src={edit} alt="" />
                        </a>
                        <a href="./">
                            <img src={pull} alt="" />
                        </a>
                        <a href="./">
                            <img src={trash} alt="" />
                        </a>
                    </div>
                </article>
                <article className="articleItem">
                    <h3>Projeto</h3>
                    <div className="articleLink">
                        <a href="#" onClick={toggleFormPopOut}>
                            <img src={edit} alt="" />
                        </a>
                        <a href="./">
                            <img src={pull} alt="" />
                        </a>
                        <a href="./">
                            <img src={trash} alt="" />
                        </a>
                    </div>
                </article>
                <article className="articleItem">
                    <h3>Projeto</h3>
                    <div className="articleLink">
                        <a href="#" onClick={toggleFormPopOut}>
                            <img src={edit} alt="" />
                        </a>
                        <a href="./">
                            <img src={pull} alt="" />
                        </a>
                        <a href="./">
                            <img src={trash} alt="" />
                        </a>
                    </div>
                </article>
                <article className="articleItem">
                    <h3>Projeto</h3>
                    <div className="articleLink">
                        <a href="#" onClick={toggleFormPopOut}>
                            <img src={edit} alt="" />
                        </a>
                        <a href="./">
                            <img src={pull} alt="" />
                        </a>
                        <a href="./">
                            <img src={trash} alt="" />
                        </a>
                    </div>
                </article>
                <article className="articleItem">
                    <h3>Projeto</h3>
                    <div className="articleLink">
                        <a href="#" onClick={toggleFormPopOut}>
                            <img src={edit} alt="" />
                        </a>
                        <a href="./">
                            <img src={pull} alt="" />
                        </a>
                        <a href="./">
                            <img src={trash} alt="" />
                        </a>
                    </div>
                </article>
                <article className="articleItem">
                    <h3>Projeto</h3>
                    <div className="articleLink">
                        <a href="#" onClick={toggleFormPopOut}>
                            <img src={edit} alt="" />
                        </a>
                        <a href="./">
                            <img src={pull} alt="" />
                        </a>
                        <a href="./">
                            <img src={trash} alt="" />
                        </a>
                    </div>
                </article>
            </article>
        </main>
    );
};

export default Projetos;

