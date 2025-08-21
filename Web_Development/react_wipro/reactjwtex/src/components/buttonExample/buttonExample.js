import React, {Component} from 'react';
import './buttonExample.scss'
// import { connect } from "react-redux";
// import { bindActionCreators } from "redux";
// import * as buttonExampleActions from "../../store/buttonExample/actions";
export default class buttonExample extends Component {
    // constructor(props) {
    //     super(props);
    //     this.state = {};
    // }
    render() {
      return <div className="component-button-example">Hello! component buttonExample</div>;
    }
  }
// export default connect(
//     ({ buttonExample }) => ({ ...buttonExample }),
//     dispatch => bindActionCreators({ ...buttonExampleActions }, dispatch)
//   )( buttonExample );