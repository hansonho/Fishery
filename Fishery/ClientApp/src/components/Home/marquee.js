import React, { useState, useEffect } from 'react';

function Marquee() {

    const [marqueeData, setMarqueeData] = useState("");

    useEffect(() => {
        fetch('api/Marquees')
            .then(response => {
                return response.text();
            })
            .then(text => {
                setMarqueeData(text);
            })
            .catch(err => {
                console.error(err);
            });
    });

    return (
        <div className="marquee-info">
            <div className="item">
                <span>{marqueeData}</span>
            </div>
        </div>
    );
}

export default Marquee;
