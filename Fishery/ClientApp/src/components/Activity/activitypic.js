import React from 'react';

import pic from '../../media/Activity/Activity1.jpg';

function Activitypic() {
    return (
        <div className="activity-info">
            <div className="item">
                <img src={pic} alt="activity" />
            </div>
        </div>
    );
}

export default Activitypic;
