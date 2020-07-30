import React from 'react';

import Weather from './weather';
import Datetime from './datetime';

function Header() {
    return(
        <header className="header-info">
            <Weather />
            <Datetime />
        </header>
    );
}

export default Header;
