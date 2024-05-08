import "./Footer.css";
import logo from "../assets/img/Vulture_Art.svg";

export default function Footer() {
    return (
        <div className="footerContainer">
            <div>
                <a className="logoFooter" href="#">
                    <img src={logo} alt="" />
                </a>
            </div>
            <nav className="navFooterContainer">
                <a href="#">Lorem Ipsum</a>
                <a href="#">Lorem Ipsum</a>
                <a href="#">Lorem Ipsum</a>
                <a href="#">Lorem Ipsum</a>
            </nav>
            <div className="direitosFooter">
                <p>© 2024, Feito com ❤️ por Eduardo Fonseca</p>
            </div>
        </div>
    );
}
