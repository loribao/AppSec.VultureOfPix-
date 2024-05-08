import React, { useState, useEffect } from "react";
import "./PageClimp.css";

const PageClimp = () => {
    const [isVisible, setIsVisible] = useState(false);

    const scrollToTop = () => {
        window.scrollTo({
            top: 0,
            behavior: "smooth",
        });
    };

    useEffect(() => {
        const handleScroll = () => {
            if (window.pageYOffset > 300) {
                setIsVisible(true);
            } else {
                setIsVisible(false);
            }
        };

        window.addEventListener("scroll", handleScroll);

        return () => {
            window.removeEventListener("scroll", handleScroll);
        };
    }, []);

    return (
        <div
            className={`scrollToTopButton ${isVisible ? "showButton" : ""}`}
            onClick={scrollToTop}
        >
            ↑
        </div>
    );
};

export default PageClimp;
