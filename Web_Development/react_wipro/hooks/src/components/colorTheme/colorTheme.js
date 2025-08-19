import React, {Component} from 'react';
import './colorTheme.css'
// import { connect } from "react-redux";
// import { bindActionCreators } from "redux";
// import * as colorThemeActions from "../../store/colorTheme/actions";
export default class colorTheme extends Component {
    // constructor(props) {
    //     super(props);
    //     this.state = {};
    // }
    render() {
      return <div className="component-color-theme">Hello! component colorTheme</div>;
    }
  }
// export default connect(
//     ({ colorTheme }) => ({ ...colorTheme }),
//     dispatch => bindActionCreators({ ...colorThemeActions }, dispatch)
//   )( colorTheme );