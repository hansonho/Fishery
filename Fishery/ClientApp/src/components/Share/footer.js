import React from 'react';
import { Link } from 'react-router-dom';

function Footer() {
    return(
        <div className="footer-info">
            <div className="item">
                <Link to="/">回首頁</Link>
            </div>
        </div>
    );
}

export default Footer;
