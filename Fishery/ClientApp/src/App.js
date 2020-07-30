import React, { Component } from 'react';
import { Route, Switch } from 'react-router';
import Home from './components/Home/index';
import Activity from './components/Activity/index';
import Photography from './components/Photography/index';
import Park from './components/Park/index';
import Header from './components/Share/header';

import './css/reset.css';
import 'slick-carousel/slick/slick.css';
import 'slick-carousel/slick/slick-theme.css';
import './css/share.css';
import './css/home.css';
import './css/activity.css';
import './css/photography.css';
import './css/park.css';

export default class App extends Component {
  static displayName = App.name;

  render() {
    return (
      <div>
        <Header />
        <Switch>
          <Route exact path="/" component={Home} />
          <Route path="/activity" component={Activity} />
          <Route path="/photography" component={Photography} />
          <Route path="/park" component={Park} />
        </Switch>
      </div>
    );
  }
}
