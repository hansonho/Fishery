import React from 'react';

import Activitypic from './activitypic';
import Header from '../Share/header';
import Footer from '../Share/footer';


function Activity() {
    return (
        <div className="activitypage">
            <Header />
            <Activitypic />
            <Footer />
        </div>
    );
}

export default Activity;
