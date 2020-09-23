import React from 'react';
import { Link } from 'react-router-dom';

function Btnlink(props) {
    const {
        className,
        picData,
    } = props;
    return (
        <div className={className}>
            {picData.map((e) => {
                if (e.src.split('.').length > 1) {
                    if (e.alt == 'livebroadcast') {
                        return (
                            <div key={e.index} className="item">
                                <a href={e.href} target={e.target} rel="noreferrer noopener">
                                    <img src={e.src} alt={e.alt} />
                                </a>
                            </div>
                        );
                    }
                    return (
                        <div key={e.index} className="item">
                            <a href={e.href}>
                                <img src={e.src} alt={e.alt} />
                            </a>
                        </div>
                    );
                }
                return (
                    <div key={e.index} className="item">
                        <a href={e.href}>
                            <img src={e.src} alt={e.alt} />
                        </a>
                    </div>
                );
            })}
        </div>
    );
}

export default Btnlink;
